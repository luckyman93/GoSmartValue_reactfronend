import { IBasketProduct } from "./interfaces/basket-product";
import { Location } from "./location";

export class BasketProduct implements IBasketProduct{
  "productId"= 1;
  "quantity"= 1;
  "price"= 0;
  "pictureUrl"= "string";
  "promoCode"= "string";
  "inputData": {
    "locationId": 1;
    "location": Location
    "localityId": 1;
    "streetId": 1;
    "streetName": "string";
    "plotNo": "string";
    "plotSize": 0;
    "plotSizeMetric": 1;
    "zoning": 1;
    "developmentPhase": 1
  }
}
