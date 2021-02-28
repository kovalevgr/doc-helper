import {Pipe, PipeTransform} from "@angular/core";
import {LOCATION_STORAGE_KEY} from "../location.service";

@Pipe({
  name: 'currentLocation',
})
export class CurrentLocationPipe implements PipeTransform {
  transform(value:any, ...args:any[]): string {
    return localStorage.getItem(LOCATION_STORAGE_KEY);
  }
}
