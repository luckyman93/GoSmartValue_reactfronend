import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class MarketService {

  _LandRatesUrl = environment.apiUrl + `/api/markets/rates/land`;
  _BuildingCostRatesURL =  environment.apiUrl + `/api/markets/rates/building-cost`;
  _BuildingMaterialCostURL = environment.apiUrl + `/api/markets/rates/building-material-cost`;
  _SalesTrendsURL = environment.apiUrl + `/api/markets/sales-trends`;

  headerDict = {
    'Authorization': "Bearer "+ this.authService.getToken(),
    'ApiKey': environment.apiKey
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
    params:{
    }
  };

  constructor(private http: HttpClient,
              private authService: AuthenticationService
              ) { }

  getLandRates() {
    return this.http.get<any[]>(`${this._LandRatesUrl}`, this.requestOptions);
  }

  getBuildingCostRates() {
    return this.http.get<any[]>(`${this._BuildingCostRatesURL}`, this.requestOptions);
  }

  getAllBuildingMaterialCosts() {
    return this.http.get<any[]>(`${this._BuildingMaterialCostURL}`,this.requestOptions);
  }

  getSalesTrends(params: any) {
    this.requestOptions.params = params;
    return this.http.get<any[]>(`${this._SalesTrendsURL}`,this.requestOptions);
  }
}
