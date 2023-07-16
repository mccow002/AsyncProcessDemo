import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { OrderActions } from "../data-access/store/orders.actions";
import { fromOrders } from "../data-access/store";
import { Observable } from "rxjs";
import { Order } from "../data-access/models";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {

  assemblyNameCtrl = new FormControl<string>('', Validators.required);

  orders$: Observable<Order[]> = this.store.select(fromOrders.getOrders);

  constructor(
    private readonly store: Store,
    private readonly toast: ToastrService
  ) {
  }

  ngOnInit() {
    this.store.dispatch(OrderActions.loadOrders());
  }

  orderTrackBy(index: number, item: Order) {
    return item.orderId;
  }

  createOrder(){
    if(this.assemblyNameCtrl.invalid) {
      return;
    }

    this.store.dispatch(OrderActions.createOrder({
      assemblyName: this.assemblyNameCtrl.value ?? ''
    }));
  }

}
