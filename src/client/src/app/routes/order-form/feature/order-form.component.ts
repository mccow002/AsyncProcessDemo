import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { ActionsSubject, Store } from "@ngrx/store";
import { OrderActions } from "../data-access/store/orders.actions";
import { fromOrders } from "../data-access/store";
import { Observable } from "rxjs";
import { Order } from "../data-access/models";
import { ToastrService } from "ngx-toastr";
import { ofType } from "@ngrx/effects";

type Mode = 'Add' | 'Edit';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {

  assemblyNameCtrl = new FormControl<string>('', Validators.required);

  mode: Mode = 'Add';
  editingOrder: Order | undefined;
  isWorking = false;

  orders$: Observable<Order[]> = this.store.select(fromOrders.getOrders);

  constructor(
    private readonly store: Store,
    private readonly actions$: ActionsSubject
  ) {
  }

  ngOnInit() {
    this.store.dispatch(OrderActions.loadOrders());

    this.actions$.pipe(
      ofType(OrderActions.createOrderOptimisticUpdate)
    ).subscribe(() => {
      this.isWorking = false;
      this.assemblyNameCtrl.setValue('');
    });
  }

  orderTrackBy(index: number, item: Order) {
    return item.orderId;
  }

  createOrder() {
    if (this.assemblyNameCtrl.invalid) {
      return;
    }

    this.isWorking = true;
    this.store.dispatch(OrderActions.createOrder({
      assemblyName: this.assemblyNameCtrl.value ?? ''
    }));
  }

  beginEdit(order: Order) {
    this.mode = 'Edit';
    this.editingOrder = order;
    this.assemblyNameCtrl.setValue(order.assemblyName);
  }

  saveOrder() {
    if (this.editingOrder && this.assemblyNameCtrl.value) {
      this.store.dispatch(OrderActions.editOrder({
        order: {
          ...this.editingOrder,
          assemblyName: this.assemblyNameCtrl.value
        }
      }));
    }

    this.mode = 'Add';
    this.editingOrder = undefined;
    this.assemblyNameCtrl.setValue('');
  }

  cancel() {
    this.mode = 'Add';
    this.editingOrder = undefined;
    this.assemblyNameCtrl.setValue('');
  }

  deleteOrder(order: Order) {
    this.store.dispatch(OrderActions.deleteOrder({orderId: order.orderId}));
  }

}
