import { Actions, ofType, createEffect } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import { OrderActions } from "./orders.actions";
import { map, switchMap } from "rxjs";
import { OrderHttpService } from "../services/order-http.service";

@Injectable()
export class OrdersEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly orderHttp: OrderHttpService
  ) {
  }

  createOrder$ = createEffect(() =>
    this.actions$.pipe(
      ofType(OrderActions.createOrder),
      switchMap(action =>
        this.orderHttp.createOrder(action.assemblyName).pipe(
          map(() => OrderActions.createOrderSuccess({
            assemblyName: action.assemblyName
          }))
        )
      )
    )
  );
}
