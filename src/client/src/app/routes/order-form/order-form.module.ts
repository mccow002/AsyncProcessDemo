import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderFormComponent } from './feature/order-form.component';
import { RouterModule, Routes } from "@angular/router";
import { ReactiveFormsModule } from "@angular/forms";
import { StoreModule } from "@ngrx/store";
import { orderFeatureName } from "./data-access/store/orders.state";
import { ordersReducers } from "./data-access/store/orders.reducers";
import { EffectsModule } from "@ngrx/effects";
import { OrdersEffects } from "./data-access/store/orders.effects";

const routes: Routes = [
  {
    path: '',
    component: OrderFormComponent
  }
];

@NgModule({
  declarations: [
    OrderFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    StoreModule.forFeature(orderFeatureName, ordersReducers),
    EffectsModule.forFeature([
      OrdersEffects
    ])
  ]
})
export class OrderFormModule { }
