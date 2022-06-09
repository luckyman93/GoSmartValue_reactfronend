import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {VendorModule} from "../vendor/vendor.module";
import { PremiumAccountsComponent } from './components/promo/premium-accounts/premium-accounts.component';
import {FormsModule} from "@angular/forms";
import { HeadingDescriptionComponent } from './components/heading-description/heading-description.component';
import { ProductComponent } from './components/product/product.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ReportSampleComponent } from './components/report-sample/report-sample.component';
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component';
import { ProfileViewComponent } from './components/profile-view/profile-view.component';
import { ProfileEditComponent } from './components/profile-edit/profile-edit.component';
import { SubscriptionComponent } from './components/subscription/subscription.component';
import { SubscriptionListComponent } from './components/subscription-list/subscription-list.component';
import { TermsAndConditionsComponent } from './components/terms-and-conditions/terms-and-conditions.component';
import { SettingsComponent } from './components/settings/settings.component';
import { OrderComponent } from './components/basket/components/order/order.component';
import { PaymentOptionsComponent } from './components/basket/components/payment-options/payment-options.component';
import { UserDetailsComponent } from './components/basket/components/user-details/user-details.component';
import { UserAddressComponent } from './components/basket/components/user-address/user-address.component';
import { ShoppingBasketComponent } from './components/basket/pages/shopping-basket/shopping-basket.component';
import { OrderItemComponent } from './components/basket/components/order-item/order-item.component';
import { TotalComponent } from './components/basket/components/total/total.component';
import { SubscriptionFeaturesComponent } from './components/subscription-features/subscription-features.component';
import { OrderHistoryComponent } from './components/basket/pages/order-history/order-history.component';
import { BuildingRatesComponent } from './components/rates/building-rates/building-rates.component';
import { LandRatesComponent } from './components/rates/land-rates/land-rates.component';
import { BuildingMaterialCostComponent } from './components/rates/building-material-cost/building-material-cost.component';
import { SalesTrendsComponent } from './components/trends/sales-trends/sales-trends.component';
import { OrderProductComponent } from './components/order-product/order-product.component';
import { SubscriptionConfirmationComponent } from './components/subscription-confirmation/subscription-confirmation.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { UserProfileComponent } from './components/users/user-profile/user-profile.component';
import { UserEditComponent } from './components/users/user-edit/user-edit.component';

@NgModule({
  declarations: [
    HeadingDescriptionComponent,
    ProductComponent,
    ProductListComponent,
    ReportSampleComponent,
    PremiumAccountsComponent,
    PrivacyPolicyComponent,
    ProfileViewComponent,
    ProfileEditComponent,
    SubscriptionComponent,
    SubscriptionListComponent,
    TermsAndConditionsComponent,
    SettingsComponent,
    OrderComponent,
    PaymentOptionsComponent,
    UserDetailsComponent,
    UserAddressComponent,
    ShoppingBasketComponent,
    OrderItemComponent,
    TotalComponent,
    SubscriptionFeaturesComponent,
    OrderHistoryComponent,
    BuildingRatesComponent,
    LandRatesComponent,
    BuildingMaterialCostComponent,
    SalesTrendsComponent,
    OrderProductComponent,
    SubscriptionConfirmationComponent,
    UserListComponent,
    UserProfileComponent,
    UserEditComponent
  ],
  imports: [
    CommonModule,
    VendorModule,
    FormsModule
  ],
    exports: [
        CommonModule,
        VendorModule,
        HeadingDescriptionComponent,
        ProductListComponent,
        PremiumAccountsComponent,
        SubscriptionListComponent,
        SubscriptionComponent,
        SubscriptionFeaturesComponent
    ]
})
export class SharedModule { }
