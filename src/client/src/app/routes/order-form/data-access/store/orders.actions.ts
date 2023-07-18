import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Order } from "../models";
import { Update } from '@ngrx/entity';

export const OrderActions = createActionGroup({
  source: 'Orders',
  events: {
    'Load Orders': emptyProps(),
    'Load Orders Success': props<{ orders: Order[] }>(),
    'Create Order': props<{ assemblyName: string }>(),
    'Create Order Optimistic Update': props<{ assemblyName: string, tempId: number }>(),
    'Create Order Error': props<{ tempId: number }>(),

    'Edit Order': props<{ orderId: number, assemblyName: string }>(),
    'Edit Order Optimistic Update': props<{ order: Order }>(),
    'Edit Order Error': props<{ order: Order }>(),

    'Delete Order': props<{ orderId: number }>(),
    'Delete Order Optimistic Update': props<{ orderId: number }>(),
    'Delete Order Error': props<{ order: Order }>(),

    'Add Order': props<{ order: Order, tempId?: number }>(),
    'Update Order': props<{ changes: Update<Order> }>(),
    'Remove Order': props<{ orderId: number }>()
  }
});
