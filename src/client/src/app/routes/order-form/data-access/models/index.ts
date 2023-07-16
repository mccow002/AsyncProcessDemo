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
