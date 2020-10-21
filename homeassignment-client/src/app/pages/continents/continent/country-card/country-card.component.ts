import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'country-card',
  templateUrl: './country-card.component.html',
  styleUrls: ['./country-card.component.scss']
})
export class CountryCardComponent implements OnInit {

  @Input() country;
  constructor( ) { }
   
  ngOnInit(): void {


    //  this.countryFlagURL = this.http.get("https://www.countryflags.io/"+this.country.emoji+"/shiny/64.png")
    //  .pipe(catchError(this.handleError()));
    //  this.countryFlag.subscribe(
    //   ()=> "https://www.countryflags.io/"+this.country.emoji+"/shiny/64.png");
  }

 

}
