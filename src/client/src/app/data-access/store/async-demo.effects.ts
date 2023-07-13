import { Injectable } from "@angular/core";
import { HubService } from "../services/hub.service";
import { Actions, createEffect, ofType, OnInitEffects } from "@ngrx/effects";
import { Action, createAction, INIT } from "@ngrx/store";
import { defer, mergeMap, switchMap } from "rxjs";

export const startHub = createAction('Start Hub');

@Injectable()
export class AsyncDemoEffects implements OnInitEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly hub: HubService
  ) {
  }

  init$ = createEffect(() =>
    this.actions$.pipe(
      ofType(startHub),
      switchMap(() => {
        console.log('STARTING HUB');
        return this.hub.start();
      })
    ),
    {dispatch: false}
  );

  ngrxOnInitEffects(): Action {
    return startHub();
  }

}
