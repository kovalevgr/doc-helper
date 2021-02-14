import {Injectable} from '@angular/core';
import {Counter} from './counter';
import {COUNTER} from './mock-counter';

@Injectable({
  providedIn: 'root',
})
export class HomeCounterService {
  private readonly _counter: Counter;

  constructor() {
    this._counter = HomeCounterService.getCounter();
  }

  get counter(): Counter {
    return this._counter;
  }

  private static getCounter(): Counter {
    return COUNTER;
  }
}
