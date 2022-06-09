import { Component, OnInit } from '@angular/core';
import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {StandardValuationService} from "../../../../services/standard-valuation.service";
import {IInstantValuationReportRequest} from "../../model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../../model/instant-valuation";
import {Router} from "@angular/router";
import {LocationService} from "../../../../services/location.service";

@Component({
  selector: 'app-reporty-type',
  templateUrl: './reporty-type.component.html',
  styleUrls: ['./reporty-type.component.scss']
})
export class ReportyTypeComponent implements OnInit {
  instantValuationReportRequest: IInstantValuationReportRequest = new InstantValuationReportRequest();
  submitted: boolean = false;
  companies: any[] = [];
  reportType!: string;


  isDetailed: boolean = false;

  constructor(
    public instantValuation: InstantValuationComponent,
    private valuationService: StandardValuationService,
    private router: Router,
    private locationService: LocationService) {
    this.instantValuation.activeIndex = 1
  }

  ngOnInit(): void {
    this.instantValuationReportRequest = this.valuationService.getReportRequest();
  }

  nextPage(type: string) {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    this.router.navigate(["/summary"]);
  }

  previousPage() {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    this.router.navigate(["/property-features"]);
  }

  isDetailedReport(type: boolean)
  {
      this.isDetailed = type;
      if(type == false)
      {
        
      }
  }

  getLocations(){
    this.locationService.GetLocations().subscribe(res => {
      this.companies = res;
    })
  }


}
