import {ILocation} from "./location";

export interface IBasketProduct{
  "productId": number,
  "quantity": number,
  "price": number,
  "pictureUrl": string,
  "promoCode": string,
  "inputData": {
    "locationId": number,
    "location": ILocation,
    "localityId": number,
    "streetId": number,
    "streetName": string,
    "plotNo": string,
    "plotSize": number,
    "plotSizeMetric": number,
    "zoning": number,
    "developmentPhase": number
  }
}
