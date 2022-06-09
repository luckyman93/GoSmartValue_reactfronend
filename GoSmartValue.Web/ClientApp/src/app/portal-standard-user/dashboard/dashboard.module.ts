import { NgModule } from '@angular/core';
import {MainDashComponent} from "./main-dash/main-dash.component";
import {SharedModule} from "../../shared/shared.module";
import {DashboardRoutingModule} from "./dashboard.routing";



@NgModule({
  declarations: [
    MainDashComponent
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
