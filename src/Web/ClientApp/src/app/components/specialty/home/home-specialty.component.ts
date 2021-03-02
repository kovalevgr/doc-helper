import {Component, OnInit} from '@angular/core';

import {Observable} from "rxjs";
import {map} from "rxjs/operators";

import {Specialty} from '../specialty';
import {SpecialtyService} from '../specialty.service';
import {LocationService} from "../../location/location.service";
import {BaseComponent} from "../../base-component";

@Component({
  selector: 'app-home-specialty',
  templateUrl: './home-specialty.component.html',
})
export class HomeSpecialtyComponent extends BaseComponent implements OnInit {
  specialities$: Observable<Specialty[]>;

  constructor(
    readonly locationService: LocationService,
    private readonly specialtyService: SpecialtyService
  ) {
    super();
  }

  ngOnInit(): void {
    this.specialities$ = this.specialtyService.getSpecialty();
  }

  public showAllSpecialties() {
    this.specialities$
      .pipe(
        map(speciality => speciality.forEach((specialty) => { specialty.active = true; }))
      );
  }
}
