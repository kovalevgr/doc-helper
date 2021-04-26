import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {map} from "rxjs/operators";

import {Doctor} from "./doctor";
import {WebApiClient} from "../../web-api-client";

@Injectable({
  providedIn: 'root',
})
export class DoctorsService {
  constructor(private readonly client: WebApiClient) {}

  getList(): Observable<Doctor[]> {
    return this.client.get("/api/Doctors")
      .pipe(map((data: [object]) => data.map((doctor) => Doctor.deserialize(doctor))));
  }
}
