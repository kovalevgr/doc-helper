import {Location} from "../location";

export class LocationListDto {
  currentLocation: Location | undefined;
  locations: Location[] = [];
}
