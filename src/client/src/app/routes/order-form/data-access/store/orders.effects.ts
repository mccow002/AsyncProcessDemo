import { Actions, ofType, createEffect } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { OrderActions } from "./orders.actions";
import { map, switchMap } from "rxjs";
import { OrderHttpService } from "../services/order-http.service";
import { domainChangeAction } from "../../../../data-access/store/async-demo.actions";
import {
  DeleteOrderError,
  OrderCreatedErrorNotification,
  OrderCreatedNotification, OrderDeletedNotification,
  OrderUpdatedNotification,
  UpdateOrderError
} from "../models";

@Injectable()
export class OrdersEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly orderHttp: OrderHttpService
  ) {
  }

  loadOrders$ = createEffect(() =>
    this.actions$.pipe(
      ofType(OrderActions.loadOrders),
      switchMap(() =>
        this.orderHttp.loadOrders().pipe(
          map(rsp => OrderActions.loadOrdersSuccess({orders: rsp}))
        )
      )
    )
  );

  createOrder$ = createEffect(() =>
    this.actions$.pipe(
      ofType(OrderActions.createOrder),
      switchMap(action => {
          const tempId = Math.ceil((Math.random() * -1) * 100);
          return this.orderHttp.createOrder(action.assemblyName, tempId).pipe(
            map(() => OrderActions.createOrderOptimisticUpdate({
              assemblyName: action.assemblyName,
              tempId: tempId
            }))
          );
        }
      )
    )
  );

  updateOrder$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(OrderActions.editOrder),
        switchMap(action => {
            return this.orderHttp.updateOrder(action.order.orderId, action.order.assemblyName).pipe(
              map(() => OrderActions.editOrderOptimisticUpdate({
                order: action.order
              }))
            );
          }
        )
      );
    }
  );

  deleteOrder$ = createEffect(() => {
      return this.actions$.pipe(
        ofType(OrderActions.deleteOrder),
        switchMap(action => {
            return this.orderHttp.deleteOrder(action.orderId).pipe(
              map(() => OrderActions.deleteOrderOptimisticUpdate({
                orderId: action.orderId
              }))
            );
          }
        )
      );
    }
  );

  domainEvents$ = createEffect(() =>
    this.actions$.pipe(
      ofType(domainChangeAction),
      map(event => {
        switch (event.clientNotification.eventName) {
          case 'OrderCreated':{
            const payload = event.clientNotification.payload as OrderCreatedNotification;
            return OrderActions.orderAdded({
              order: payload.order,
              tempId: payload.tempId
            });
          }
          case 'OrderCreatedError': {
            const payload = event.clientNotification.payload as OrderCreatedErrorNotification;
            return OrderActions.createOrderError({
              tempId: payload.tempId
            });
          }
          case 'OrderEdited': {
            const payload = event.clientNotification.payload as OrderUpdatedNotification;
            return OrderActions.orderUpdated({
              changes: {
                id: payload.order.orderId,
                changes: {
                  ...payload.order
                }
              }
            });
          }
          case 'EditOrderError': {
            const payload = event.clientNotification.payload as UpdateOrderError;
            return OrderActions.editOrderError({
              order: payload.order
            });
          }
          case 'OrderDeleted': {
            const payload = event.clientNotification.payload as OrderDeletedNotification;
            return OrderActions.orderDeleted({
              orderId: payload.orderId
            });
          }
          case 'DeleteOrderError': {
            const payload = event.clientNotification.payload as DeleteOrderError;
            return OrderActions.deleteOrderError({
              order: payload.order
            });
          }
          default:
            return { type: 'DO NOTHING' };
        }
      })
    )
  );
}
