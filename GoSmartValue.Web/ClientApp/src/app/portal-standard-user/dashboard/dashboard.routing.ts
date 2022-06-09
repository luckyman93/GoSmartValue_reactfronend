import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {MainDashComponent} from "./main-dash/main-dash.component";
import {AuthenticationGuard} from "../../auth/guard/authentication.guard";

const routes: Routes = [
  {
    path: '',
    component: MainDashComponent,
    canActivate: [AuthenticationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
