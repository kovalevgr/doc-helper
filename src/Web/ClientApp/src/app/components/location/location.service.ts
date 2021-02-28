import {Injectable} from '@angular/core';
import {map, take} from 'rxjs/operators';

import {Location} from './location';
import {WebApiClient} from "../../web-api-client";
import {Observable} from "rxjs";

export const LOCATION_STORAGE_KEY = 'location';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  locations$: Observable<Location[]>

  constructor(private client: WebApiClient) {
    this.locations$ = this.getLocations();
  }

  getCurrentLocation(): Observable<Location> {
    return this.locations$
      .pipe(
        map(locations => locations.find(location => this.userLocation() == location.city))
      )
      .pipe(take(1))
  }

  userLocation(): string {
    return localStorage.getItem(LOCATION_STORAGE_KEY);
  }

  private getLocations(): Observable<Location[]> {
    return this.client.get("/api/Cities")
      .pipe(
        map((data: [object]) => data.map((location) => Location.deserialize(location)))
      );
  }
}
