import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewInstructionsComponent } from './components/instructions/view-instructions/view-instructions.component';
import { SharedModule } from '../shared/shared.module';
import { PortalValuerRoutingModule } from './portal-valuer-routing.module';
import { ValuationModule } from '../shared/components/valuation/valuation.module';
import { CompletedInstructionsComponent } from './components/instructions/completed-instructions/completed-instructions.component';
import { RejectedInstructionsComponent } from './components/instructions/rejected-instructions/rejected-instructions.component';
import { PendingInstructionsComponent } from './components/instructions/pending-instructions/pending-instructions.component';
import { NewInstructionsComponent } from './components/instructions/new-instructions/new-instructions.component';
import { SubscriptionListComponent } from './components/subscriptions/subscription-list/subscription-list.component';

@NgModule({
  declarations: [
    ViewInstructionsComponent,
    CompletedInstructionsComponent,
    RejectedInstructionsComponent,
    PendingInstructionsComponent,
    NewInstructionsComponent,
    SubscriptionListComponent
  ],
  imports: [
    CommonModule,
    PortalValuerRoutingModule,
    SharedModule,
    ValuationModule
  ]
})
export class PortalValuerModule { }
