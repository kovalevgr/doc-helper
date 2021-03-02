import { Component } from '@angular/core';

import {BaseComponent} from "./components/base-component";
import {Location} from "./components/location/location";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent {
  constructor() {
    super();

    this.initLocation();
  }

  private initLocation(): void {
    let location = Location.deserialize({ name: this.userLocation().toLowerCase() });

    this.setUserLocation(location);
  }
}
