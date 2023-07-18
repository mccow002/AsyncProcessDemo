import { Action, createReducer, on } from '@ngrx/store';
import { orderAdapter, OrderState } from "./orders.state";
import { OrderActions } from "./orders.actions";

const reducers = createReducer(
  orderAdapter.getInitialState(),

  on(OrderActions.loadOrdersSuccess, (state, { orders }) => {
    return orderAdapter.setAll(orders, state);
  }),

  on(OrderActions.createOrderOptimisticUpdate, (state, action) => {
    return orderAdapter.addOne({
      orderId: action.tempId,
      assemblyName: action.assemblyName,
      partCount: 3
    }, state);
  }),

  on(OrderActions.orderAdded, (state, { order, tempId }) => {
    if(tempId && state.entities[tempId]) {
      state = orderAdapter.removeOne(tempId, state);
    }

    return orderAdapter.addOne(order, state);
  }),

  on(OrderActions.createOrderError, (state, { tempId }) => {
    return orderAdapter.removeOne(tempId, state);
  }),

  on(OrderActions.editOrderOptimisticUpdate, (state, { order }) => {
    return orderAdapter.updateOne({
      id: order.orderId,
      changes: {
        ...order
      }
    }, state);
  }),

  on(OrderActions.editOrderError, (state, { order }) => {
    return orderAdapter.updateOne({
      id: order.orderId,
      changes: {
        ...order
      }
    }, state);
  }),

  on(OrderActions.orderUpdated, (state, { changes }) => {
    return orderAdapter.updateOne(changes, state);
  }),

  on(OrderActions.deleteOrderOptimisticUpdate, (state, { orderId }) => {
    return orderAdapter.removeOne(orderId, state);
  }),

  on(OrderActions.deleteOrderError, (state, { order }) => {
    return orderAdapter.addOne(order, state);
  }),

  on(OrderActions.orderDeleted, (state, { orderId }) => {
    return orderAdapter.removeOne(orderId, state);
  }),
);

export function ordersReducers(state: OrderState | undefined, action: Action) {
  return reducers(state, action);
}
