import { NgModule } from '@angular/core';
import { HomeComponent } from './pages/home/home.component';
import { ContactUsComponent } from './pages/contact-us/contact-us.component';
import {HomeRoutingModule} from "./home-routing.module";
import { WhyUsComponent } from './components/why-us/why-us.component';
import {FlexModule} from "@angular/flex-layout";
import { OurTeamComponent } from './components/our-team/our-team.component';
import { OurTeamMemberComponent } from './components/our-team-member/our-team-member.component';
import { JoinUsComponent } from './components/join-us/join-us.component';
import {SharedModule} from "../shared/shared.module";
import {VendorModule} from "../vendor/vendor.module";
import { ResidentialValuationComponent } from './components/residential-valuation/residential-valuation.component';
import {ValuationModule} from "../shared/components/valuation/valuation.module";
import { GetInTouchComponent } from './components/get-in-touch/get-in-touch.component';
import { ContactDetailsComponent } from './components/contact-details/contact-details.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { ValuationsComponent } from './pages/valuations/valuations.component';
import { PromoSectionComponent } from './components/promo-section/promo-section.component';
import {SubscriptionsComponent} from "./pages/subscriptions/subscriptions.component";
import { HomeSubscriptionsComponent } from './components/home-subscriptions/home-subscriptions.component';

@NgModule({
    declarations: [
      HomeComponent,
      ContactUsComponent,
      WhyUsComponent,
      OurTeamComponent,
      OurTeamMemberComponent,
      JoinUsComponent,
      GetInTouchComponent,
      ContactDetailsComponent,
      OurTeamMemberComponent,
      ReportsComponent,
      ValuationsComponent,
      ResidentialValuationComponent,
      ReportsComponent,
      PromoSectionComponent,
      SubscriptionsComponent,
      HomeSubscriptionsComponent
    ],
    exports: [
        HomeComponent
    ],
    imports: [
      HomeRoutingModule,
      FlexModule,
      SharedModule,
      VendorModule,
      ValuationModule,
      HomeRoutingModule,
    ]
})
export class HomeModule { }
