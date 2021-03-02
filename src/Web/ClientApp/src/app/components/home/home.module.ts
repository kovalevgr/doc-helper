import {AngularSvgIconModule} from "angular-svg-icon";
import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {RouterModule} from '@angular/router';

import {HomeComponent} from './home.component';
import {HomeCounterComponent} from './counter/home-counter.component';
import {HomeSearchComponent} from '../search/home/home-search.component';
import {HomeSpecialtyComponent} from '../specialty/home/home-specialty.component';
import {CurrentLocationPipe} from "../location/pipes/current-location.pipe";

@NgModule({
    imports: [
      AngularSvgIconModule,
      CommonModule,
        FormsModule,
        RouterModule.forChild([{path: '', component: HomeComponent}]),
    ],
  exports: [RouterModule],
  declarations: [
    HomeComponent,
    HomeSearchComponent,
    HomeCounterComponent,
    HomeSpecialtyComponent,

    CurrentLocationPipe
  ]
})
export class HomeModule {}
