import { NgModule } from '@angular/core';
import {RouterModule, Routes} from "@angular/router";
import {LocationDetailsComponent} from "./components/location-details/location-details.component";
import {PropertyDetailsComponent} from "./components/property-details/property-details.component";
import {PropertyFeaturesComponent} from "./components/property-features/property-features.component";
import {ReportyTypeComponent} from "./components/reporty-type/reporty-type.component";
import {InstantValuationComponent} from "./instant-valuation/instant-valuation.component";
import {SummaryComponent} from "./components/summary/summary.component";
import { IssueInstructionComponent } from './components/instructions/issue-instruction/issue-instruction.component';

const routes : Routes = [
  {
    path:'',
    component: InstantValuationComponent,
    children:[
      {path:'', redirectTo: 'location-details', pathMatch: 'full'},
      {path: 'location-details', component: LocationDetailsComponent},
      {path: 'property-details', component: PropertyDetailsComponent},
      {path: 'property-features', component: PropertyFeaturesComponent},
      {path: 'report-type', component: ReportyTypeComponent},
      {path: 'summary', component: SummaryComponent}
    ]},
    {
      path:'issue-instruction',
      component: IssueInstructionComponent
    }
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ValuationRoutingModule { }
