import { Injectable } from '@angular/core';
import {Configuration} from "../models/configurations";
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {ApiConfigurationService} from "./api-configuration.service";
import {ErrorHandlerService} from "../error-handler/error-handler.service";
import {Observable} from "rxjs";
import {ILocation} from "../models/interfaces/location";
import {ILocality} from "../models/interfaces/locality";
import {InstantValuationReportRequest} from "../components/valuation/model/instant-valuation";
import {IInstantValuationReportRequest} from "../components/valuation/model/interface/instant-valuation";

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  _locationUrl = environment.apiUrl + `/api/locations`;
  isLoggedIn:boolean = true;

  headerDict = {
    'apiKey': environment.apiKey
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
  };

  constructor(private http: HttpClient) {}

  GetLocations(): Observable<ILocation[]> {
    return this.http.get<ILocation[]>(`${this._locationUrl}/GetLocations`, this.requestOptions);
  }
  GetLocalities(location: ILocation): Observable<ILocality[]> {
    return this.http.get<ILocality[]>(
      `${this._locationUrl}/GetLocalities?locationId=${location.id}&locationName=${location.name}`, this.requestOptions);
  }

}
