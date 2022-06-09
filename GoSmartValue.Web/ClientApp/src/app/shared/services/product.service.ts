import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {IProduct} from "../models/interfaces/product";
import {IPackage} from "../models/interfaces/package";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  _productUrl = environment.apiUrl + `/api/products`;
  _packagesUrl = environment.apiUrl + `/api/packages`;

  headerDict = {
    'apiKey': environment.apiKey
  }

  zohoRequest={ 
    headers: new HttpHeaders( {
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin':'*'
    })
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
  };

  constructor(private http: HttpClient) { }

  getProducts(){
    return this.http.get<IProduct[]>(`${this._productUrl}`, this.requestOptions)
  }

  getPackages(){
    return this.http.get<IPackage[]>(`${this._packagesUrl}`, this.requestOptions)
  }

  getInstructions(){
    return this.http.get('assets/data/instructions.json');
  }
  
  sendToZoho(body: any){
    var zohoUrl = 'https://cors-anywhere.herokuapp.com/https://forms.zohopublic.com/vantagepropertiesbw/form/LocationReport/formperma/eecxEXXonfTPd0ewaLW-mm0eRcD7-JZkJyj4DfGVgcU/htmlRecords/submit'
    
    return this.http.post(zohoUrl,body);
  }
}
