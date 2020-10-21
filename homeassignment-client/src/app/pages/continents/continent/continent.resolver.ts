import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Resolve, Router, ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store'; 

import {  of } from 'rxjs';
import { Continent } from 'src/app/models/continent.model';
import { LoadContinents, LoadSingleContinent } from 'src/app/store/actions/continent.actions';
import * as continentsReducers from '../../../store/reducers/continents.reducer';

@Injectable({
  providedIn: 'root'
})
export class ContinentResolver implements Resolve<any> {

  constructor(
    private store: Store<{ continents: continentsReducers.State }>,
    private route: ActivatedRoute,
    private router: Router 
      ){
    
  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(route.data){
        this.store.dispatch({ type: LoadSingleContinent , continentCode: route.params['continentCode'] });
    }
    return of('NONE');
  }

  
}
