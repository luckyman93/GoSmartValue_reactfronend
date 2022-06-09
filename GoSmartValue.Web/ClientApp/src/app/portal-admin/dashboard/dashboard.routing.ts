import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {AuthenticationGuard} from "../../auth/guard/authentication.guard";

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthenticationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
