import {
  ActivatedRoute,
  ActivatedRouteSnapshot,
  CanActivate, ParamMap, Router,
  RouterStateSnapshot
} from '@angular/router';

import {LocationService} from '../location/location.service';
import {Observable} from 'rxjs';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocationGuard implements CanActivate {
  constructor(private router: Router, private location: LocationService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    const params = route.paramMap;
    const city = params.get('city');

    if (!city) {
      this.redirectIfException('City must be set');
    }

    this.location.getLocationByCity(city)
      .subscribe((location) => {
        if (!location) {
          this.redirectIfException(`City ${city} not found`);
        }
      });

    return true;
  }

  private redirectIfException(text: string) {
    this.router.navigateByUrl('not-found').then(r => console.log(text));
  }
}
