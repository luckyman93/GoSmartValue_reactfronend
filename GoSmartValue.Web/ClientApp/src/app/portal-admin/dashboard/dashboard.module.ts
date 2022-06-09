import { NgModule } from '@angular/core';
import {SharedModule} from "../../shared/shared.module";
import {DashboardRoutingModule} from "./dashboard.routing";

@NgModule({
  declarations: [
  ],
  imports: [
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
