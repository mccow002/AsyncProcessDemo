import { Injectable } from '@angular/core';
import * as signalr from '@microsoft/signalr';
import { HubConnectionState, RetryContext } from '@microsoft/signalr';
import { concatMap, from, Observable, of, retry, timer } from "rxjs";

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

  public start(): Observable<void | unknown> {
    return of({}).pipe(
      concatMap(() => {
        if(this.hubConnection.state === HubConnectionState.Disconnected) {
          return from(this.hubConnection.start());
        } else if(this.hubConnection.state === HubConnectionState.Connecting) {
          throw new Error('Still Connecting');
        }

        return of({});
      }),
      retry({count: 5, delay: () => timer(500)})
    );
  }
}
