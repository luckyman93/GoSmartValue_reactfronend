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
export class BasketService {
  _basketURL = environment.apiUrl + `/api/baskets`;

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

  addToBasket(basketItem : IBasketProduct){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    let requestData = [basketItem];
    return this.http.post<IBasketProduct[]>(`${this._basketURL}`+"/current/items", requestData, requestOptions_loggedIn)
  }
  removeFromBasket(itemId : any){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    return this.http.delete<any>(`${this._basketURL}`+"/current/items/"+itemId, requestOptions_loggedIn)
  }

  getCurrentBasket(){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    return this.http.get<IBasket>(`${this._basketURL}`+"/current", requestOptions_loggedIn)
  }

  confirmBasket(userId: string){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    let requestData = {
      'UserId': userId
    }
    return this.http.put<any>(`${this._basketURL}`+"/current/confirm", requestData ,requestOptions_loggedIn)
  }

  getPaymentToken(){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    let requestData = {}
    return this.http.put<IPaymentToken>(`${this._basketURL}`+"/current/payment-token", requestData ,requestOptions_loggedIn)
  }

  getAllBaskets(){
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.authService.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    return this.http.get<IBasket>(`${this._basketURL}`+"/all", requestOptions_loggedIn)
  }

}
