import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {HomeComponent} from "./pages/home/home.component";
import {ContactUsComponent} from "./pages/contact-us/contact-us.component";
import {NewsComponent} from "./pages/news/news.component";
import {SubscriptionsComponent} from "./pages/subscriptions/subscriptions.component";
import {ValuationsComponent} from "./pages/valuations/valuations.component";
import {ReportsComponent} from "./pages/reports/reports.component";
import {PrivacyPolicyComponent} from "../shared/components/privacy-policy/privacy-policy.component";
import {ProfileViewComponent} from "../shared/components/profile-view/profile-view.component";
import {ProfileEditComponent} from "../shared/components/profile-edit/profile-edit.component";
import {TermsAndConditionsComponent} from "../shared/components/terms-and-conditions/terms-and-conditions.component";
import {SettingsComponent} from "../shared/components/settings/settings.component";
import {ShoppingBasketComponent} from "../shared/components/basket/pages/shopping-basket/shopping-basket.component";
import { OrderHistoryComponent } from '../shared/components/basket/pages/order-history/order-history.component';
import { LandRatesComponent } from '../shared/components/rates/land-rates/land-rates.component';
import { BuildingRatesComponent } from '../shared/components/rates/building-rates/building-rates.component';
import { BuildingMaterialCostComponent } from '../shared/components/rates/building-material-cost/building-material-cost.component';
import { SalesTrendsComponent } from '../shared/components/trends/sales-trends/sales-trends.component';
import {AuthenticationGuard} from "../auth/guard/authentication.guard";
import {ChangePasswordComponent} from "../auth/components/change-password/change-password.component";
import {UserListComponent} from "../shared/components/users/user-list/user-list.component";

const routes : Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'contact-us',
    component: ContactUsComponent
  },
  {
    path: 'news',
    component: NewsComponent
  },
  {
    path: 'reports',
    component: ReportsComponent
  },
  {
    path: 'subscriptions',
    component: SubscriptionsComponent
  },
  {
    path: 'valuations',
    component: ValuationsComponent
  },
  {
    path: 'privacy-policy',
    component: PrivacyPolicyComponent
  },
  {
    path: 'terms-and-conditions',
    component: TermsAndConditionsComponent
  },
  {
    path: 'profile',
    component: ProfileViewComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'profile/edit',
    component: ProfileEditComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'new-password',
    component: ChangePasswordComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'settings',
    component: SettingsComponent,
    canActivate: [AuthenticationGuard]
  },
  {
    path: 'basket',
    component: ShoppingBasketComponent
  },
  {
    path: 'order-history',
    component: OrderHistoryComponent
  },
  {
    path: 'land-rates',
    component: LandRatesComponent
  },
  {
    path: 'building-costs',
    component: BuildingRatesComponent
  },
  {
    path: 'admin/user-list',
    component: UserListComponent
  },
  {
    path: 'building-material-costs',
    component: BuildingMaterialCostComponent
  },
  {
    path: 'sales-trends',
    component: SalesTrendsComponent
  }
]


@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)]
})
export class HomeRoutingModule { }
