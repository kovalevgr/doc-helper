import {Injectable} from '@angular/core';

import {SPECIALTY} from './mock-specialty';
import {Observable, of} from "rxjs";
import {Specialty} from "./specialty";

@Injectable({
  providedIn: 'root',
})
export class SpecialtyService {
  private readonly _specialty;

  constructor() {
    this._specialty = getSelection();
  }

  public getSpecialty(): Observable<Specialty[]> {
    return of(SPECIALTY);
  }
}
