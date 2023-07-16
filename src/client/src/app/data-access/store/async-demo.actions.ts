import { createAction, props } from "@ngrx/store";
import { ClientNotification } from "../models";

export const domainChangeAction = createAction(
  'Domain Change Event',
  props<{ clientNotification: ClientNotification }>()
);
