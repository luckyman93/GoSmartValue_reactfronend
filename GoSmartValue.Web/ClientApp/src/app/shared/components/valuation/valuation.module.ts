import { NgModule } from '@angular/core';
import {ValuationRoutingModule} from "./valuation-routing";
import {InstantValuationComponent} from "./instant-valuation/instant-valuation.component";
import {LocationDetailsComponent} from "./components/location-details/location-details.component";
import {PropertyDetailsComponent} from "./components/property-details/property-details.component";
import {PropertyFeaturesComponent} from "./components/property-features/property-features.component";
import {ReportyTypeComponent} from "./components/reporty-type/reporty-type.component";
import {VendorModule} from "../../../vendor/vendor.module";
import {FormsModule} from "@angular/forms";
import {AuthModule} from "../../../auth/auth.module";
import { SummaryComponent } from './components/summary/summary.component';
import {SharedModule} from "../../shared.module";
import { InstantValuationListComponent } from './instant-valuation-list/instant-valuation-list.component';
import { IssueInstructionComponent } from './components/instructions/issue-instruction/issue-instruction.component';
import { InstructionListComponent } from './components/instructions/instruction-list/instruction-list.component';
import { UploadValuationComponent } from './components/instructions/upload-valuation/upload-valuation.component';

@NgModule({
  declarations: [
    InstantValuationComponent,
    LocationDetailsComponent,
    PropertyDetailsComponent,
    PropertyFeaturesComponent,
    ReportyTypeComponent,
    SummaryComponent,
    InstantValuationListComponent,
    IssueInstructionComponent,
    InstructionListComponent,
    UploadValuationComponent
  ],
    imports: [
        ValuationRoutingModule,
        VendorModule,
        FormsModule,
        AuthModule,
        SharedModule,
    ],
  exports: [
    InstantValuationComponent,
    InstructionListComponent
    
  ]
})
export class ValuationModule { }
