import {
    createReducer, 
    on,
    Action
  } from '@ngrx/store';
  import {Continent} from '../../models/continent.model'
  import * as ContinentsActions from '../actions/continent.actions';
  
  
  export interface State {
    Continents: Continent[];
    isContinentsLoaded?:boolean;
    singleContinent: Continent;
    isSingleContinentLoaded?:boolean;
  }
  
  export const initialState: State = {
    Continents: [],
    isContinentsLoaded: false,
    singleContinent: null,
    isSingleContinentLoaded: false,
  };
  
  const ContinentsReducer = createReducer(
    initialState,
    on(ContinentsActions.loadContinents, state => ({ ...state, isContinentsLoaded: false })),
    on(ContinentsActions.loadContinentsSuccess, (state, { payload } ) => ({ ...state, Continents:payload, isContinentsLoaded:true })),
    on(ContinentsActions.loadSingleContinent, state => ({ ...state, singleContinent: null, isSingleContinentLoaded: false })),
    on(ContinentsActions.loadSingleContinentSuccess, (state, { payload } ) => ({ ...state, singleContinent: payload, isSingleContinentLoaded: true }))
    );
  
  export function reducer(state: State | undefined, action: Action) {
    return ContinentsReducer(state, action);
  }
  
  