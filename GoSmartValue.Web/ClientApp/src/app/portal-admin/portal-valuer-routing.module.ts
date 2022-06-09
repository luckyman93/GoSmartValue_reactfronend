import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {AuthenticationGuard} from "../auth/guard/authentication.guard";

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
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
      },
      {
        path: 'valuation',
        loadChildren: () => import('../shared/components/valuation/valuation.module').then(m => m.ValuationModule)
      }
    ]
  }
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)]
})
export class PortalValuerRoutingModule { }
