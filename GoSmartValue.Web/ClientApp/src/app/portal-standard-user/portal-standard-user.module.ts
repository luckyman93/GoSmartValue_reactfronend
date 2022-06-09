import { NgModule } from '@angular/core';
import {PortalStandardUserRoutingModule} from "./portal-standard-user-routing.module";
import { PortalLayoutComponent } from './components/portal-layout/portal-layout.component';
import { SubscriptionListComponent } from './components/subscriptions/subscription-list/subscription-list.component';
import { SharedModule } from '../shared/shared.module';
import { ValuationModule } from '../shared/components/valuation/valuation.module';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    PortalLayoutComponent,
    SubscriptionListComponent
  ],
  imports: [
    CommonModule,
    PortalStandardUserRoutingModule,
    SharedModule,
    ValuationModule
  ]
})
export class PortalStandardUserModule { }
