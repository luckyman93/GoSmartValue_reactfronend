import { IProduct} from "./interfaces/product";

export class Product implements IProduct{
  "productId" = 0;
  "name"= "";
  "description"= "";
  "type"= 0;
  "category"= 1;
  "price"= 0;
  "serviceType"= "";
  "sampleUrl"= "";
  "features": [
    {
      "id": 0;
      "pRoductId": 0;
      "title": "";
      "description": "";
      "position": 0
    }
  ]
}
