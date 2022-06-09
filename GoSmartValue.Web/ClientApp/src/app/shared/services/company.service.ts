import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {IProduct} from "../models/interfaces/product";
import {IBasketProduct} from "../models/interfaces/basket-product";
import {AuthenticationService} from "./authentication.service";
import {IBasket} from "../models/interfaces/basket";
import {Basket} from "../models/basket";
import {IPaymentToken} from "../models/interfaces/payment-token";

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  _basketURL = environment.apiUrl + `/api/Companies`;

  headerDict = {
    'apiKey': environment.apiKey
  }

  headerDict_loggedIn = {
    'Authorization': "Bearer "+ this.authService.getToken(),
    'apiKey': environment.apiKey
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
  };

  requestOptions_loggedIn = {
    headers: new HttpHeaders (this.headerDict_loggedIn),
  };

  constructor(private http: HttpClient, private authService: AuthenticationService) { }

  getAllCompanies(){
    return this.http.get<any>(`${this._basketURL}`, this.requestOptions_loggedIn)
  }

}
