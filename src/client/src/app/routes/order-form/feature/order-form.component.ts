import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { OrderActions } from "../data-access/store/orders.actions";

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {

  assemblyNameCtrl = new FormControl<string>('', Validators.required);

  constructor(
    private readonly store: Store
  ) {
  }

  ngOnInit() {
    this.store.dispatch(OrderActions.loadOrder());
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
