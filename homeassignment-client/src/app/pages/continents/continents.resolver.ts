import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Resolve } from '@angular/router';
import { Store } from '@ngrx/store'; 

import {  of } from 'rxjs';
import { Continent } from 'src/app/models/continent.model';
import { LoadContinents } from 'src/app/store/actions/continent.actions';
import * as continentsReducers from '../../store/reducers/continents.reducer';

@Injectable({
  providedIn: 'root'
})
export class ContinentsResolver implements Resolve<any> {

  /**
   *
   */
  constructor(
    private store: Store<{ continents: continentsReducers.State }> 
  ){
    
  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(route.data){
      this.store.dispatch({ type: LoadContinents});
    }
    return of('NONE');
  }

  
}
