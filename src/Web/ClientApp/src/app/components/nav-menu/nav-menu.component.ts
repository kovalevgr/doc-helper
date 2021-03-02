import {Component, OnInit} from '@angular/core';
import {Observable} from "rxjs";

import {BaseComponent} from "../base-component";
import {LocationService} from "../location/location.service";
import {Location} from "../location/location";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent extends BaseComponent implements OnInit {

  isDropped: boolean = false;
  locations: Observable<Location[]>;

  constructor(readonly locationService: LocationService) {
    super();
  }

  changeDropped(): void {
    this.isDropped = !this.isDropped;
  }

  changeLocation(location: Location) {
    this.setUserLocation(location);
  }

  ngOnInit(): void {
    this.locations = this.locationService.locations$;
  }
}
