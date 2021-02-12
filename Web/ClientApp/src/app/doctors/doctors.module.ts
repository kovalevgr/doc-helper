import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import {DoctorsListComponent} from './list/doctors-list.component';

import {DoctorsRoutingModule} from './doctors-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    DoctorsRoutingModule
  ],
  declarations: [
    DoctorsListComponent
  ]
})
export class DoctorsModule {}
