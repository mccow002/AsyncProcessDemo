export type ClientNotification = {
  eventName: string;
  payload: unknown;
}

export type SuccessNotification = {
  successMessage: string;
}

export type ErrorNotification = {
  notification: ClientNotification;
  errorMessage: string;
  exception: string;
}
