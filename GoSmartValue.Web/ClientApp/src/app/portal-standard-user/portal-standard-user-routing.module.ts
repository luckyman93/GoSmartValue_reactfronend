import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {AuthenticationGuard} from "../auth/guard/authentication.guard";
import {PortalLayoutComponent} from "./components/portal-layout/portal-layout.component";
import { SubscriptionListComponent } from './components/subscriptions/subscription-list/subscription-list.component';

const routes: Routes = [
  {
    path: 'portal',
    canActivate: [AuthenticationGuard],
    children: [
      {
        path: 'portal',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'valuation',
        loadChildren: () => import('../shared/components/valuation/valuation.module').then(m => m.ValuationModule)
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
export class PortalStandardUserRoutingModule { }
