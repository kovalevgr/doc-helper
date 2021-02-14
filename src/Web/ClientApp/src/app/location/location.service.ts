import {Injectable} from '@angular/core';
import {Observable, of} from 'rxjs';

import {Location} from './location';
import {LOCATIONS} from './mock-locations';
import {filter, find, map} from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class LocationService {
  private defaultLocation = 'kiev';

  private _location: Location;

  constructor() {
    const userLocation = localStorage.getItem('location');

    this.getLocations()
      .pipe(map((locations: Location[]) => locations.find(
        location => location.city === userLocation ? userLocation : this.defaultLocation
      )))
      .subscribe(location => console.log(location));
  }

  get location(): Location {
    return this._location;
  }

  getLocations(): Observable<Location[]> {
    return of(LOCATIONS);
  }

  getLocationByCity(city: string): Observable<Location[] | undefined> {
    return this.getLocations()
      .pipe(
        find(location => (location.find(loc => loc.city === city) !== undefined))
      );
  }
}
