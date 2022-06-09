import { Component, OnInit } from '@angular/core';
import {ReportService} from "../../../../../services/report.service";
import {Router} from "@angular/router";
import {LocationService} from "../../../../../services/location.service";
import {ILocation} from "../../../../../models/interfaces/location";
import {ILocality} from "../../../../../models/interfaces/locality";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {StandardValuationService} from "../../../../../services/standard-valuation.service";
import {CompanyService} from "../../../../../services/company.service";
import {developmentStage} from "../../../../../constants/constants";
import {Locality} from "../../../../../models/locality";
import {MessageService} from 'primeng/api';
import {IInstructionRequest} from "../../../model/interface/instruction-request";
import {InstructionRequest} from "../../../model/instruction-request";

@Component({
  selector: 'app-issue-instruction',
  templateUrl: './issue-instruction.component.html',
  styleUrls: ['./issue-instruction.component.scss']
})
export class IssueInstructionComponent implements OnInit {

  // instantValuationReportRequest!: IInstantValuationReportRequest;
  instructionRequest!: IInstructionRequest;
  submitted: boolean = false;
  locations: ILocation[] = [];
  localities: ILocality[] = [];
  valuers: any[] = [];
  companies: any[] = [];
  developmentStages = developmentStage;
  detailedValuationForm: FormGroup = new FormGroup({});

  constructor(private reportService: ReportService,
              private router: Router,
              private locationService: LocationService,
              private valuationService: StandardValuationService,
              private companyService: CompanyService,
              private fb: FormBuilder,
              private messageService: MessageService) {
  }

  ngOnInit(): void {
    this.getLocations();
    this.getValuers();
    this.getCompanies();
    this.initForm();
    this.instructionRequest = new InstructionRequest();
  }

  private initForm(){
    this.detailedValuationForm = this.fb.group({
      location: ['', [Validators.required]],
      localityId: ['', [Validators.required]],
      plotNumber: ['', [Validators.required]],
      accessContactName: ['', Validators.required],
      accessContactMobileNumber: ['', Validators.required],
    })
  }

  get f(): { [key: string]: AbstractControl } { return this.detailedValuationForm.controls; }

  getLocations(){
    this.locationService.GetLocations().subscribe(res => {
      this.locations = res;
    })
  }

  getLocalities(){
    this.locationService.GetLocalities(this.instructionRequest.location).subscribe(res => {
      if(this.instructionRequest.localityId > 0) {
        this.localities = [];
        if (this.instructionRequest.locality.locationId != this.instructionRequest.location.id) {
          this.instructionRequest.locality = new Locality();
        }
      }
      this.localities = res;
    })
  }

  getValuers() {
    this.valuationService.getValuers().subscribe(res => {
      this.valuers = res;
    });
  }

  submit() {
    if (this.instructionRequest.location.name != '' && this.instructionRequest.plotNumber != '') {
        if(this.localities.length != 0 && this.instructionRequest.locality.name == '')
         {
          this.messageService.add({key: 'tc', severity:'error', summary: 'Ward', detail: 'Please provide your City/Town'});
         }
         else {

         }
    }

      if(this.instructionRequest.location.name == '')
      {
        this.messageService.add({key: 'tc', severity:'error', summary: 'City/Town', detail: 'Please provide your City/Town'});
        return;
      }

      this.instructionRequest.locationName = this.instructionRequest.location.name;
      this.instructionRequest.locationId = this.instructionRequest.location.id;
      this.instructionRequest.localityName = this.instructionRequest.locality.name;
      this.instructionRequest.localityId = this.instructionRequest.locality.id;
      this.instructionRequest.clientName = this.instructionRequest.accessContactName;
      this.instructionRequest.clientMobileNumber = this.instructionRequest.accessContactMobileNumber;

      this.submitted = true;
      console.log("my request =>", this.instructionRequest)
    this.issueInstruction(this.instructionRequest);
  }

  getCompanies(){
    this.companyService.getAllCompanies().subscribe(res => {
      this.companies = res;
    })
  }

  issueInstruction(instruction:any){
    this.valuationService.issueInstruction(instruction).subscribe(res => {
      console.log("instruction issued");
    })
  }

}
