import {Component, OnInit} from '@angular/core';

import {Specialty} from '../specialty';
import {SpecialtyService} from '../specialty.service';

@Component({
  selector: 'app-home-specialty',
  templateUrl: './home-specialty.component.html',
})
export class HomeSpecialtyComponent implements OnInit {
  private _specialties: Specialty[];

  constructor(private readonly specialtyService: SpecialtyService) {}

  ngOnInit(): void {
    this._specialties = this.specialtyService.getSpecialty();
  }

  get specialties(): Specialty[] {
    return this._specialties;
  }

  public showAllSpecialties() {
    this.specialties.forEach((specialty) => { specialty.active = true; });
  }
}
