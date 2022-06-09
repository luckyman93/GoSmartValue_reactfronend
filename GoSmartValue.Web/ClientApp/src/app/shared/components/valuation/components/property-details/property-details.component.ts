import { Component, OnInit } from '@angular/core';
import {ReportService} from "../../../../services/report.service";
import {Router} from "@angular/router";
import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {IInstantValuationReportRequest} from "../../model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../../model/instant-valuation";
import {StandardValuationService} from "../../../../services/standard-valuation.service";
import {propertyDetail} from "../../../../constants/constants";

@Component({
  selector: 'app-property-details',
  templateUrl: './property-details.component.html',
  styleUrls: ['./property-details.component.scss']
})
export class PropertyDetailsComponent implements OnInit {

  instantValuationReportRequest: IInstantValuationReportRequest = new InstantValuationReportRequest();
  submitted: boolean = false;
  propertyDetails = propertyDetail;

  constructor(
    public instantValuation: InstantValuationComponent,
    private valuationService: StandardValuationService,
    private router: Router) {
    this.instantValuation.activeIndex = 1;
  }

  ngOnInit(): void {
    this.instantValuationReportRequest = this.valuationService.getReportRequest();
  }

  nextPage() {
    if (
      this.instantValuationReportRequest.bedRooms != null 
      && this.instantValuationReportRequest.Kitchens != null 
      && this.instantValuationReportRequest.SittingRooms!= null 
      && this.instantValuationReportRequest.bathRooms!= null 
      && this.instantValuationReportRequest.toilets!= null 
      && this.instantValuationReportRequest.garages!= null ) {

        this.valuationService.saveReportRequest(this.instantValuationReportRequest);
        this.router.navigate(["/property-features"]);  
    }
      this.submitted = true;
    
  }

  previousPage() {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    this.router.navigate(["/location-details"]);
  }

}
