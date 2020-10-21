import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Continent } from 'src/app/models/continent.model';
import { LoadSingleContinent } from '../../../store/actions/continent.actions';
import { ActivatedRoute, Router } from '@angular/router';
import * as continentsReducers from '../../../store/reducers/continents.reducer';
import { Observable } from 'rxjs';
import { state } from '@angular/animations';

@Component({
  selector: 'app-continent',
  templateUrl: './continent.component.html',
  styleUrls: ['./continent.component.scss']
})
export class ContinentComponent implements OnInit {

  continent : Continent;
  isLoading:boolean;
  constructor(    
    private store: Store<{ continents: continentsReducers.State }>,
    private route: ActivatedRoute,
    private router: Router 
    ) {
      
    }


ngOnInit() {
this.isLoading= true;
  this.store.subscribe(state=> {
    this.continent = state.continents.singleContinent;
    this.isLoading = !state.continents.isSingleContinentLoaded;
  });
  }

  backButton(){
    this.router.navigate([''], { relativeTo: this.route.parent })

  }

}
