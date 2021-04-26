import {Component, OnInit} from '@angular/core';

import {BaseComponent} from "../../base-component";
import {Observable} from "rxjs";
import {Doctor} from "../doctor";
import {DoctorsService} from "../doctors.service";

@Component({
  selector: 'app-doctor-list',
  styleUrls: ['./doctors-list.component.css'],
  templateUrl: './doctors-list.component.html',
})
export class DoctorsListComponent extends BaseComponent implements OnInit {
  doctors$: Observable<Doctor[]>

  constructor(private readonly doctorsService: DoctorsService) {
    super();
  }

  ngOnInit(): void {
    this.doctors$ = this.doctorsService.getList();
  }
}
