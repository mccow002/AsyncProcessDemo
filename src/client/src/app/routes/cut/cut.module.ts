import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CutComponent } from './feature/cut.component';
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
  {
    path: '',
    component: CutComponent
  }
];

@NgModule({
  declarations: [
    CutComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class CutModule { }
