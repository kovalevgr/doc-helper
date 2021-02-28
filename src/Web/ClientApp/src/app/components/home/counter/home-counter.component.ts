import {Component, OnInit} from '@angular/core';
import {Counter} from './counter';

import {HomeCounterService} from './home-counter.service';

@Component({
  selector: 'app-home-counter',
  templateUrl: './home-counter.component.html',
})
export class HomeCounterComponent implements OnInit {
  counter: Counter;

  constructor(private counterService: HomeCounterService) { }

  ngOnInit(): void {
    this.counter = this.counterService.counter;
  }
}
