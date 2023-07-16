import { Actions, ofType, createEffect } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { OrderActions } from "./orders.actions";
import { map, switchMap } from "rxjs";
import { OrderHttpService } from "../services/order-http.service";
import { domainChangeAction } from "../../../../data-access/store/async-demo.actions";
import { OrderCreatedErrorNotification, OrderCreatedNotification } from "../models";

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
            map(() => OrderActions.createOrderSuccess({
              assemblyName: action.assemblyName,
              tempId: tempId
            }))
          );
        }
      )
    )
  );

  domainEvents$ = createEffect(() =>
    this.actions$.pipe(
      ofType(domainChangeAction),
      map(event => {
        switch (event.clientNotification.eventName) {
          case 'OrderCreated':{
            const payload = event.clientNotification.payload as OrderCreatedNotification;
            return OrderActions.addOrder({
              order: payload.order,
              tempId: payload.tempId
            });
          }
          case 'OrderCreatedError': {
            const payload = event.clientNotification.payload as OrderCreatedErrorNotification;
            return OrderActions.addOrderError({
              tempId: payload.tempId
            });
          }
          default:
            return { type: 'DO NOTHING' };
        }
      })
    )
  );
}
