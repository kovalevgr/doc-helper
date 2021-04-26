import {Injectable} from '@angular/core';

import {Observable, of} from "rxjs";
import {Specialty} from "./specialty";
import {WebApiClient} from "../../web-api-client";
import {map} from "rxjs/operators";

@Injectable({
  providedIn: 'root',
})
export class SpecialtyService {
  constructor(private client: WebApiClient) {}

  public getSpecialty(): Observable<Specialty[]> {
    return this.client.get("/api/Specialties")
      .pipe(
        map((data: [object]) => data.map((location) => Specialty.deserialize(location)))
      );
  }
}
