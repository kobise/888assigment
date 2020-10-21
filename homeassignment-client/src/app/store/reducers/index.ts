import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer
} from '@ngrx/store';
import { environment } from '../../../environments/environment';
import * as continentsReducers from './continents.reducer';

export interface State {

}

export const reducers: ActionReducerMap<State> = {
  continents: continentsReducers.reducer
};


export const metaReducers: MetaReducer<State>[] = !environment.production ? [] : [];
