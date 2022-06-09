import { Component, OnInit } from '@angular/core';
import {ReportService} from "../../../../services/report.service";
import {Router} from "@angular/router";
import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {LocationService} from "../../../../services/location.service";
import {ILocation} from "../../../../models/interfaces/location";
import {IInstantValuationReportRequest} from "../../model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../../model/instant-valuation";
import {ILocality} from "../../../../models/interfaces/locality";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {StandardValuationService} from "../../../../services/standard-valuation.service";
import {developmentStage} from "../../../../constants/constants";
import {Locality} from "../../../../models/locality";
import {MessageService} from 'primeng/api';



@Component({
  selector: 'app-location-details',
  templateUrl: './location-details.component.html',
  styleUrls: ['./location-details.component.scss']
})
export class LocationDetailsComponent implements OnInit {

  instantValuationReportRequest!: IInstantValuationReportRequest;
  submitted: boolean = false;
  locations: ILocation[] = [];
  localities: ILocality[] = [];
  developmentStages = developmentStage;
  instantValuationForm: FormGroup = new FormGroup({});

  constructor(private reportService: ReportService,
              private router: Router,
              public instantValuation: InstantValuationComponent,
              private locationService: LocationService,
              private valuationService: StandardValuationService,
              private fb: FormBuilder,
              private messageService: MessageService) {
    if(router.url.includes("location-details")){
      this.instantValuation.activeIndex = 1;
    }
  }

  ngOnInit(): void {
    this.getLocations();
    this.initForm();
    this.instantValuationReportRequest = this.valuationService.getReportRequest();
  }

  private initForm(){
    this.instantValuationForm = this.fb.group({
      location: ['', [Validators.required]],
      localityId: ['', [Validators.required]],
      streetName: ['', [Validators.required]],
      plotNo: ['', [Validators.required]],
      plotSize: ['', Validators.required]
    })
  }

  get f(): { [key: string]: AbstractControl } { return this.instantValuationForm.controls; }

  getLocations(){
    this.locationService.GetLocations().subscribe(res => {
      this.locations = res;
    })
  }

  getLocalities(){
    this.locationService.GetLocalities(this.instantValuationReportRequest.location).subscribe(res => {
      if(this.instantValuationReportRequest.localityId > 0) {
        this.localities = [];
        if (this.instantValuationReportRequest.locality.locationId != this.instantValuationReportRequest.location.id) {
          this.instantValuationReportRequest.locality = new Locality();
        }
      }
      this.localities = res;
    })
  }

  nextPage() {
    if (this.instantValuationReportRequest.location.name != ''
        && this.instantValuationReportRequest.plotNo != '' 
        && this.instantValuationReportRequest.plotSize != 0 
        && this.instantValuationReportRequest.propertyType != 1) {

          if(this.localities.length != 0 && this.instantValuationReportRequest.locality.name == '')
           {
            this.messageService.add({key: 'tc', severity:'error', summary: 'Ward', detail: 'Please provide your City/Town'});
           } 
           else {

            this.instantValuationReportRequest.locationId = this.instantValuationReportRequest.location.id;
            this.instantValuationReportRequest.localityId = this.instantValuationReportRequest.locality.id;
            this.valuationService.saveReportRequest(this.instantValuationReportRequest);
            this.instantValuation.activeIndex +=1;
            this.router.navigate(["/property-details"]);
            return;

           }
   
    }

      if(this.instantValuationReportRequest.location.name == '')
      {
        this.messageService.add({key: 'tc', severity:'error', summary: 'City/Town', detail: 'Please provide your City/Town'});
        return;
      }

      if(this.instantValuationReportRequest.plotSize == 0)
      {
        this.messageService.add({key: 'tc', severity:'error', summary: 'Plot Size', detail: 'Please provide your Plot Size'});
        return;
      }

      if(this.instantValuationReportRequest.propertyType == 1)
      {
        this.messageService.add({key: 'tc', severity:'error', summary: 'Development Stage', detail: 'Please provide your Development Stage'});
        return;
      }

      this.submitted = true;
  }

}
