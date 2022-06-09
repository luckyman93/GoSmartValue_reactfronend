export interface IBasket{
  "createdOn": string,
  "updatedOn": string,
  "id": number,
  "invoiceId": number,
  "orderNo": string,
  "promoCode": string,
  "buyerId": string,
  "netTotal": number,
  "discountTotal": number,
  "grossTotal": number,
  "status": number,
  "items": any
}
