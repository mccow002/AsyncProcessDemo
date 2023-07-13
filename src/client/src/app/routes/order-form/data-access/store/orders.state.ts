import { createEntityAdapter, EntityState } from "@ngrx/entity";
import { Order } from "../models";

export const orderFeatureName = 'orders';

export type OrderState = EntityState<Order>;

export const orderAdapter = createEntityAdapter<Order>({
  selectId: x => x.orderId
});
