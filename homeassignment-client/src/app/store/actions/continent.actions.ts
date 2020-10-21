import { createAction, props } from '@ngrx/store';
import { Continent } from 'src/app/models/continent.model';

export const LoadContinents:string ='[Load Continents] Load Continents';
export const LoadContinentsSuccess:string ='[Load Continents] Load Continents Success';

export const LoadSingleContinent:string ='[Load Single Continent] Load  Single Continent';
export const LoadSingleContinentSuccess:string ='[Load  Single Continent] Load  Single Continent Success';

export const loadContinents = createAction(
    LoadContinents
);

export const loadContinentsSuccess = createAction(
    LoadContinentsSuccess,
    props< { payload: Continent[] }>()
);

export const loadSingleContinent = createAction(
    LoadSingleContinent,
    props< { continentCode: string }>()
);

export const loadSingleContinentSuccess = createAction(
    LoadSingleContinentSuccess,
    props< { payload: Continent }>()
);