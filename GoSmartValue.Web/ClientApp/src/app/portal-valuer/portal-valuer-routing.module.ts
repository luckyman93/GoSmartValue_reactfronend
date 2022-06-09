import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {AuthenticationGuard} from "../auth/guard/authentication.guard";
import { ViewInstructionsComponent } from './components/instructions/view-instructions/view-instructions.component';
import { SubscriptionListComponent } from './components/subscriptions/subscription-list/subscription-list.component';

const routes: Routes = [
  {
    path: 'valuer',
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: 'valuer',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        component: ViewInstructionsComponent
      },
      {
        path: 'valuation',
        loadChildren: () => import('../shared/components/valuation/valuation.module').then(m => m.ValuationModule)
      },
      {
        path:'instructions',
        component: ViewInstructionsComponent
      },
      {
        path:'subscriptions',
        component: SubscriptionListComponent
      }
    ]
  }
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)]
})

export class PortalValuerRoutingModule { }