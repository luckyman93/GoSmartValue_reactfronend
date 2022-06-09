import { Component, OnInit } from '@angular/core';
import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {StandardValuationService} from "../../../../services/standard-valuation.service";
import {IInstantValuationReportRequest} from "../../model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../../model/instant-valuation";
import {features} from "../../../../constants/constants";
import {Router} from "@angular/router";

@Component({
  selector: 'app-property-features',
  templateUrl: './property-features.component.html',
  styleUrls: ['./property-features.component.scss']
})
export class PropertyFeaturesComponent implements OnInit {
  instantValuationReportRequest: IInstantValuationReportRequest = new InstantValuationReportRequest();
  submitted: boolean = false;
  propertyFeatures = features;

  constructor(
    public instantValuation: InstantValuationComponent,
    private valuationService: StandardValuationService,
    private router: Router) {
    this.instantValuation.activeIndex = 1
  }

  ngOnInit(): void {
    this.instantValuationReportRequest = this.valuationService.getReportRequest();
  }

  nextPage() {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    this.router.navigate(["/report-type"]);
  }

  previousPage() {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    this.router.navigate(["/property-details"]);
  }
}
