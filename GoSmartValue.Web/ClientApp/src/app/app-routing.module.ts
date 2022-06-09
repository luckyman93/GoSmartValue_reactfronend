import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'portal',
    loadChildren: () => import('./portal-standard-user/portal-standard-user.module').then(m => m.PortalStandardUserModule)
  },
  {
    path: 'corporate',
    loadChildren: () => import('./portal-corporate/portal-corporate.module').then(m => m.PortalCorporateModule)
  },
  {
    path: 'valuer',
    loadChildren: () => import('./portal-valuer/portal-valuer.module').then(m => m.PortalValuerModule)
  },
  {
    path: 'valuation',
    loadChildren: () => import('./shared/components/valuation/valuation.module').then(m => m.ValuationModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
