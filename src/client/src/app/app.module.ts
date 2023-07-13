import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StoreModule } from "@ngrx/store";
import { StoreDevtoolsModule } from "@ngrx/store-devtools";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ConnectionIdInterceptor } from "./data-access/interceptors/connection-id.interceptor";
import { EffectsModule } from "@ngrx/effects";
import { AsyncDemoEffects } from "./data-access/store/async-demo.effects";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    StoreModule.forRoot(),
    StoreDevtoolsModule.instrument({
      name: 'AsyncDemo'
    }),
    EffectsModule.forRoot([
      AsyncDemoEffects
    ])
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ConnectionIdInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
