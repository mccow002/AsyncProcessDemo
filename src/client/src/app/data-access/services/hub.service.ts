import { Injectable } from '@angular/core';
import * as signalr from '@microsoft/signalr';
import { HubConnectionState, RetryContext } from '@microsoft/signalr';
import { concatMap, from, Observable, of, retry, timer } from "rxjs";
import { ClientNotification, ErrorNotification, SuccessNotification } from "../models";
import { Store } from "@ngrx/store";
import { domainChangeAction } from "../store/async-demo.actions";
import { ToastrService } from "ngx-toastr";

@Injectable({
  providedIn: 'root'
})
export class HubService {

  public hubConnection = new signalr.HubConnectionBuilder()
    .withUrl('https://localhost:44330/signalr')
    .withAutomaticReconnect({
      nextRetryDelayInMilliseconds(retryContext: RetryContext): number | null {
        return 10000;
      }
    })
    .build();

  constructor(
    private readonly store: Store,
    private readonly toast: ToastrService
  ) {
    this.hubConnection.on('notification', (event: ClientNotification) => {
      this.store.dispatch(domainChangeAction({clientNotification: event}));
    });

    this.hubConnection.on('success', (msg: SuccessNotification) => {
      this.toast.success(msg.successMessage);
    });

    this.hubConnection.on('error', (msg: ErrorNotification) => {
      this.toast.error(msg.errorMessage);
      console.error(msg.exception);
      this.store.dispatch(domainChangeAction({clientNotification: msg.notification}));
    });
  }

  public start(): Observable<void | unknown> {
    return of({}).pipe(
      concatMap(() => {
        if (this.hubConnection.state === HubConnectionState.Disconnected) {
          return from(this.hubConnection.start());
        } else if (this.hubConnection.state === HubConnectionState.Connecting) {
          throw new Error('Still Connecting');
        }

        return of({});
      }),
      retry({count: 5, delay: () => timer(500)})
    );
  }
}
