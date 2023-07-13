import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HubService } from "../services/hub.service";
import { HubConnectionState } from "@microsoft/signalr";

@Injectable()
export class ConnectionIdInterceptor implements HttpInterceptor {

  constructor(
    private readonly hub: HubService
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    if(request.method !== 'GET') {
      const header: Record<string, string> = {};
      if(this.hub.hubConnection.state === HubConnectionState.Connected) {
        header['connectionId'] = this.hub.hubConnection.connectionId ?? '';
      }

      const newReq = request.clone({
        setHeaders: header
      });

      return next.handle(newReq);
    }

    return next.handle(request);
  }
}
