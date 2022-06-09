import {IBasket} from "./interfaces/basket";

export class Basket implements IBasket{
  "createdOn"= "0001-01-01T00:00:00+00:00";
  "updatedOn"= "0001-01-01T00:00:00+00:00";
  "id"= 0;
  "invoiceId"= 0;
  "orderNo"= "";
  "promoCode"= "";
  "buyerId"= "08d9c3ff-d554-4883-8609-a6d41a734dfd";
  "netTotal"= 0;
  "discountTotal"= 0;
  "grossTotal"= 0;
  "status"= 0;
  "items" = [
    {
      "createdDate": "2021-12-24T17:25:17.629867+00:00",
      "createdBy": "00000000-0000-0000-0000-000000000000",
      "updatedDate": "2021-12-24T17:25:17.629862+00:00",
      "updatedBy": "00000000-0000-0000-0000-000000000000",
      "id": 1,
      "basketId": 1,
      "productId": 1,
      "productName": "Standard 175",
      "unitPrice": 175,
      "discount": 0,
      "quantity": 1,
      "price": 1,
      "pictureUrl": "string",
      "promoCode": "string",
      "inputDataId": 0,
      "inputData": ""
    }
  ]
}
