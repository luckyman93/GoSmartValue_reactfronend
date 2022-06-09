import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {PortalCorporateRoutingModule} from './portal-corporate-routing.module';
import { PendingInstructionsComponent } from './components/instructions/pending-instructions/pending-instructions.component'
import { SharedModule } from '../shared/shared.module';
import { CompletedInstructionsComponent } from './components/instructions/completed-instructions/completed-instructions.component';
import { RejectedInstructionsComponent } from './components/instructions/rejected-instructions/rejected-instructions.component';
import { ViewInstructionsComponent } from './components/instructions/view-instructions/view-instructions.component';
import { ValuationModule } from '../shared/components/valuation/valuation.module';
import { SubscriptionListComponent } from './components/subscriptions/subscription-list/subscription-list.component';

@NgModule({
  declarations: [
        PendingInstructionsComponent,
        CompletedInstructionsComponent,
        RejectedInstructionsComponent,
        ViewInstructionsComponent,
        SubscriptionListComponent
  ],
  imports: [
    CommonModule,
    PortalCorporateRoutingModule,
    SharedModule,
    ValuationModule
  ]
})
export class PortalCorporateModule { }
