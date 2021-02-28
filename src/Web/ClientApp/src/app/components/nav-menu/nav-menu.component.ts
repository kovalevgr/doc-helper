import {Component, OnInit} from '@angular/core';
import {LocationService} from "../location/location.service";
import {Location} from "../location/location";
import {Observable} from "rxjs";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isDropped: boolean = false;
  locations: Observable<Location[]>;

  constructor(readonly locationService: LocationService) { }

  changeDropped(): void {
    this.isDropped = !this.isDropped;
  }

  changeLocation(location: Location) {
    // this.locationService.changeCurrentLocation(location);
  }

  ngOnInit(): void {
    this.locations = this.locationService.locations$;
  }
}
