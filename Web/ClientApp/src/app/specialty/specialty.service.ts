import {Injectable} from '@angular/core';

import {SPECIALTY} from './mock-specialty';

@Injectable({
  providedIn: 'root',
})
export class SpecialtyService {
  private readonly _specialty;

  constructor() {
    this._specialty = getSelection();
  }

  public getSpecialty() {
    return SPECIALTY;
  }
}
