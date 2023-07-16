import { Action, createReducer, on } from '@ngrx/store';
import { orderAdapter, OrderState } from "./orders.state";
import { OrderActions } from "./orders.actions";

const reducers = createReducer(
  orderAdapter.getInitialState(),

  on(OrderActions.loadOrdersSuccess, (state, { orders }) => {
    return orderAdapter.setAll(orders, state);
  }),

  on(OrderActions.createOrderSuccess, (state, action) => {
    return orderAdapter.addOne({
      orderId: action.tempId,
      assemblyName: action.assemblyName,
      partCount: 3
    }, state);
  }),

  on(OrderActions.addOrder, (state, { order, tempId }) => {
    if(tempId && state.entities[tempId]) {
      state = orderAdapter.removeOne(tempId, state);
    }

    return orderAdapter.addOne(order, state);
  }),

  on(OrderActions.addOrderError, (state, { tempId }) => {
    return orderAdapter.removeOne(tempId, state);
  })
);

export function ordersReducers(state: OrderState | undefined, action: Action) {
  return reducers(state, action);
}
