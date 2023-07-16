import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Order } from "../models";

export const OrderActions = createActionGroup({
  source: 'Orders',
  events: {
    'Load Orders': emptyProps(),
    'Load Orders Success': props<{ orders: Order[] }>(),
    'Create Order': props<{ assemblyName: string }>(),
    'Create Order Success': props<{ assemblyName: string, tempId: number }>(),
    'Add Order': props<{ order: Order, tempId?: number }>(),
    'Add Order Error': props<{ tempId: number }>()
  }
});
