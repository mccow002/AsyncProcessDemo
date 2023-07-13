import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BendComponent } from './feature/bend.component';
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
  {
    path: '',
    component: BendComponent
  }
];

@NgModule({
  declarations: [
    BendComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class BendModule { }
