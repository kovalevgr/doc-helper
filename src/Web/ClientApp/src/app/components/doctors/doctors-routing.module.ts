import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {DoctorsListComponent} from "./list/doctors-list.component";
import {LocationGuard} from "../../guards/location.guard";

const doctorsRoutes: Routes = [
  { path: ':city', component: DoctorsListComponent, canActivate: [LocationGuard] },
  { path: ':city/:district', component: DoctorsListComponent, canActivate: [LocationGuard] },
  { path: ':city/:district/:specialty', component: DoctorsListComponent, canActivate: [LocationGuard] },
];

@NgModule({
  imports: [
    RouterModule.forChild(doctorsRoutes)
  ],
  exports: [RouterModule]
})
export class DoctorsRoutingModule { }
