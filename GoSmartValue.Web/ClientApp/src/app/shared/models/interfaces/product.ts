export interface IProduct{
  "name": string,
  "description": string,
  "type": number,
  "category": number,
  "price": number,
  "serviceType": string,
  "sampleUrl": string,
  "features": [
    {
      "id": number,
      "pRoductId": number,
      "title": string,
      "description": string,
      "position": number
    }
  ]
}
