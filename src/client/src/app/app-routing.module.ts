import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'order-form',
    loadChildren: () => import('./routes/order-form/order-form.module').then(m => m.OrderFormModule)
  },
  {
    path: 'cut',
    loadChildren: () => import('./routes/cut/cut.module').then(m => m.CutModule)
  },
  {
    path: 'bend',
    loadChildren: () => import('./routes/bend/bend.module').then(m => m.BendModule)
  },
  {path: '', redirectTo: 'order-form', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
