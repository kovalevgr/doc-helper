import {LOCATION_STORAGE_KEY , DEFAULT_CITY_NAME} from "./location/location.service";
import {Location} from "./location/location";

export abstract class BaseComponent {
  userLocation(): string {
    const location = localStorage.getItem(LOCATION_STORAGE_KEY);

    return location ? location : DEFAULT_CITY_NAME;
  }

  setUserLocation(location: Location): void {
    localStorage.setItem(LOCATION_STORAGE_KEY, location.city);
  }
}
