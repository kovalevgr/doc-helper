import {
  ActivatedRouteSnapshot,
  CanActivate, Router,
  RouterStateSnapshot
} from '@angular/router';

import {LocationService} from '../components/location/location.service';
import {Observable} from 'rxjs';
import {Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocationGuard implements CanActivate {
  constructor(private router: Router, private location: LocationService) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    const city = route.paramMap.get('city');
    if (!city) {
      this.redirectIfException('City must be set');
    }

    this.location
      .locations$
       .pipe()
      .subscribe((result) => {
        if (!result) {
          this.redirectIfException(`City ${city} not found`);
        }
      });

    return true;
  }

  private redirectIfException(text: string) {
    this.router.navigateByUrl('not-found').then(r => console.log(text));
  }
}
