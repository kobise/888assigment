import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { EMPTY, of } from 'rxjs';
import { map, mergeMap, catchError, concatMap, exhaustMap } from 'rxjs/operators';
import { HttpService } from '../http.service';
import * as ContinentsActions from '../actions/continent.actions';
import { Continent } from 'src/app/models/continent.model';

@Injectable()
export class ContinentsEffects {
 
    loadContinents$ = createEffect(() => this.actions$.pipe(
    ofType(ContinentsActions.LoadContinents),
    mergeMap(() => this.httpService.getAllContinents()
      .pipe(
        map((continents:Continent[]) => ContinentsActions.loadContinentsSuccess({ payload: continents})),
        catchError(() => EMPTY)
      ))
    )
  );

  loadContinent$ =  createEffect(() =>
  this.actions$.pipe(
    ofType(ContinentsActions.loadSingleContinent),
    exhaustMap(action =>
      this.httpService.getContinent(action.continentCode).pipe(
        map(continent => ContinentsActions.loadSingleContinentSuccess({ payload: continent })),
        catchError(() => EMPTY)
      )
    )
  )
);

  constructor(
    private actions$: Actions,
    private httpService: HttpService
  ) {}
}