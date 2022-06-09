import { Injectable } from '@angular/core';
import {IInstantValuationReportRequest} from "../components/valuation/model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../components/valuation/model/instant-valuation";
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {IResponse} from "../models/interfaces/response";
import {IUser} from "../../auth/models/interfaces/user";
import {catchError} from "rxjs/operators";
import {AuthenticationService} from "./authentication.service";
import {Location} from "../models/location";
import {Locality} from "../models/locality";
import { IDetailedValuationReportRequest } from '../components/valuation/model/interface/detailed-valuation';
import { DetailedValuationReportRequest } from '../components/valuation/model/detailed-valuation';

@Injectable({
  providedIn: 'root'
})
export class StandardValuationService {

  _valuationsUrl = environment.apiUrl + `/api/StandardValuations`;
  _reportUrl = environment.apiUrl + `/api/reports`;
  _userAccountUrl = environment.apiUrl + `/api/account/users`
  isLoggedIn:boolean = true;

  headerDict = {
    'apiKey': environment.apiKey
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
  };
  headerDict_loggedIn = {}
  requestOptions_loggedIn = {}

  instantValuationReportRequest: IInstantValuationReportRequest = new InstantValuationReportRequest();

  detailedValuationReportRequest: IDetailedValuationReportRequest = new DetailedValuationReportRequest();

  constructor(private http: HttpClient,
              private authService: AuthenticationService) {
    this.headerDict_loggedIn = {
      'Authorization': "Bearer "+ authService.getToken(),
      'apiKey': environment.apiKey
    }


    this.requestOptions_loggedIn = {
      headers: new HttpHeaders (this.headerDict_loggedIn),
    };
  }

  createInstantReportRequest(instantValuationReportRequest : IInstantValuationReportRequest){

    const request = {
      metric : instantValuationReportRequest.metric,
      plotSize : instantValuationReportRequest.plotSize,
      landUse : instantValuationReportRequest.landUse,
      propertyType : this.developmentStage(instantValuationReportRequest.propertyType),
      locationId : instantValuationReportRequest.location.id,
      localityId : instantValuationReportRequest.locality.id,
      streetName : instantValuationReportRequest.streetName,
      plotNo : instantValuationReportRequest.plotNo,
      features : this.getFeatures(instantValuationReportRequest.features),
      bathRooms : instantValuationReportRequest.bathRooms,
      toilets : instantValuationReportRequest.toilets,
      garages : instantValuationReportRequest.garages,
      bedRooms : instantValuationReportRequest.bedRooms,
      Kitchens : instantValuationReportRequest.Kitchens,
      SittingRooms : instantValuationReportRequest.SittingRooms,

    }
    return this.http.post<IResponse<any>>(`${this._reportUrl}` + "/instant/request", request, this.requestOptions_loggedIn)
  }

  developmentStage(stage: any){
    if(stage == "Undeveloped"){
      return 1;
    }
    if(stage == "Developed"){
      return 2;
    }
    return 3
  }

  getFeatures(features : any[]) {
    const valuationFeatures = [];
    for(let i = 0; i < features.length; i++){
      let selectedFeature = features[i];
      if(selectedFeature.isSelected){
        valuationFeatures.push({
          "featureType" : i
        })
      }
    }
    return valuationFeatures;
  }

  saveReportRequest(instantValuationReportRequest : IInstantValuationReportRequest){
    this.instantValuationReportRequest = instantValuationReportRequest;
  }

  getReportRequest(){
    return this.instantValuationReportRequest;
  }

  getDetailedRequest(){
    return this.detailedValuationReportRequest;
  }

  getValuers()
  {
    return this.http.get<any[]>(`${this._userAccountUrl}/valuers`, this.requestOptions);
  }

  issueInstruction(instruction: any)
  {
    return this.http.post<any>(`${this._reportUrl}` + "/instruction", instruction, this.requestOptions_loggedIn)
  }
}
