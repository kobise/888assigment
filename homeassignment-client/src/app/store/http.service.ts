import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Continent } from '../models/continent.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  url:string;

  constructor (private http: HttpClient) {
    this.url = environment.serverUrl;
  }

  getAllContinents():Observable<Continent[]> {
    return this.http.get<Continent[]>(this.url+'/api/continents');
    // return of(this.continents);
  }

  getContinent(continentCode:string) :Observable<Continent>{
    return this.http.get<Continent>(this.url+'/api/continents/'+continentCode);
    // return of(this.continent);
  }
}
