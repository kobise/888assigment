import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { Continent } from 'src/app/models/continent.model';
import * as continentsReducers from '../../store/reducers/continents.reducer';

@Component({
  selector: 'app-continents',
  templateUrl: './continents.component.html',
  styleUrls: ['./continents.component.scss']
})
export class ContinentsComponent implements OnInit {

  isLoading:boolean;

  columnDefs = [  
    { headerName: "Continent Code" ,field: 'code' },
    { headerName: "Continent Name" ,field: 'name' },
  ];

continents: Continent[];
  constructor(    
    private store: Store<{ continents: continentsReducers.State }> ,
    private route: ActivatedRoute,
    private router: Router
    ) {
      
    }

  ngOnInit(): void {
    this.isLoading= true;
    this.store.subscribe(state=> {
      this.continents = state.continents.Continents;
      this.isLoading = !state.continents.isContinentsLoaded;
    });
  }
  


  onCellClicked(event){
    this.router.navigate([event.data.code], { relativeTo: this.route })

  }
}
