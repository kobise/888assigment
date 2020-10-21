import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ContinentComponent } from './pages/continents/continent/continent.component';
import { ContinentResolver } from './pages/continents/continent/continent.resolver';
import { ContinentsComponent } from './pages/continents/continents.component';
import { ContinentsResolver } from './pages/continents/continents.resolver';


const routes: Routes = [
  { path: '', pathMatch:'full', redirectTo: '/continents'},
  { path: 'continents',resolve: {state: ContinentsResolver} ,children:[
      { path: '', component: ContinentsComponent },
      { path: ':continentCode',resolve: {state: ContinentResolver} , component: ContinentComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
