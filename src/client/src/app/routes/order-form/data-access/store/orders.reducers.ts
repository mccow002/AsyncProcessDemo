import { Action, createReducer, on } from '@ngrx/store';
import { orderAdapter, OrderState } from "./orders.state";
import { OrderActions } from "./orders.actions";

const reducers = createReducer(
  orderAdapter.getInitialState(),

  on(OrderActions.loadOrdersSuccess, (state, { orders }) => {
    return orderAdapter.setAll(orders, state);
  })
);

export function ordersReducers(state: OrderState | undefined, action: Action) {
  return reducers(state, action);
}
