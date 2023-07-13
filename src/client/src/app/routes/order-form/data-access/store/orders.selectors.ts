import { createFeatureSelector, createSelector } from "@ngrx/store";
import { orderAdapter, orderFeatureName, OrderState } from "./orders.state";

const orderFeature = createFeatureSelector<OrderState>(orderFeatureName);

export const getOrders = createSelector(
  orderFeature,
  orderAdapter.getSelectors().selectAll
);
