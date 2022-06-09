import {Component, OnDestroy, OnInit} from '@angular/core';
import {MenuItem} from "primeng/api";
import {Subscription} from "rxjs";
import {ReportService} from "../../../services/report.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-instant-valuation',
  templateUrl: './instant-valuation.component.html',
  styleUrls: ['./instant-valuation.component.scss']
})
export class InstantValuationComponent implements OnInit, OnDestroy {

  items: MenuItem[];

  // subscription: Subscription;
  activeIndex = 0;
  isHome = false;
  isLocationDetails = true;
  isSummary = true;

  constructor(
    private router: Router,
    private reportService: ReportService) {
    this.items = [];

    if(router.url.length == 1){
      this.isHome = true;
    }

    router.events.subscribe((val)=>{
      this.isLocationDetails = router.url.toString().includes("location-details");
      this.isSummary = router.url.toString().includes("summary");
    })
  }

  ngOnInit() {
    this.items = [
      {
        label: 'Location',
        routerLink: 'location-details'
      },
      {
        label: 'Property Details',
        routerLink: 'property-details'
      },
      {
        label: 'Property Features',
        routerLink: 'property-features'
      },
      {
        label: 'Report Type',
        routerLink: 'report-type'
      },
      {
        label: 'Summary',
        routerLink: 'summary'
      },
    ];


  }

  ngOnDestroy() {

  }

}
