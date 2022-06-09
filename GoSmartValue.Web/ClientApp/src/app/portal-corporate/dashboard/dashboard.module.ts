import { NgModule } from '@angular/core';
import {SharedModule} from "../../shared/shared.module";
import {DashboardRoutingModule} from "./dashboard.routing";
import { DashboardComponent } from './components/dashboard/dashboard.component';



@NgModule({
  declarations: [
  
    DashboardComponent
  ],
  exports: [
    DashboardComponent
],
  imports: [
    SharedModule,
    DashboardRoutingModule
  ]
})
export class DashboardModule { }
