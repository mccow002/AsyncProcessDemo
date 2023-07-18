export type Order = {
  orderId: number;
  assemblyName: string;
  partCount: number;
};

export type OrderCreatedNotification = {
  order: Order;
  tempId: number;
}

export type OrderCreatedErrorNotification = {
  tempId: number;
}

export type OrderUpdatedNotification = {
  order: Order;
}

export type UpdateOrderError = {
  order: Order;
}

export type OrderDeletedNotification = {
  orderId: number;
}

export type DeleteOrderError = {
  order: Order
}
