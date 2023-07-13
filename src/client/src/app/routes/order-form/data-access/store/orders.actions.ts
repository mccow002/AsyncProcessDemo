import { createActionGroup, emptyProps, props } from "@ngrx/store";
import { Order } from "../models";

export const OrderActions = createActionGroup({
  source: 'Orders',
  events: {
    'Load Order': emptyProps(),
    'Load Orders Success': props<{ orders: Order[] }>(),
    'Create Order': props<{ assemblyName: string }>(),
    'Create Order Success': props<{ assemblyName: string }>()
  }
});
