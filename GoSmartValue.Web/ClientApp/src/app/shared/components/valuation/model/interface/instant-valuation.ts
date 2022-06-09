export interface IInstantValuationReportRequest {
  metric: number;
  plotSize: number;
  landUse: number;
  propertyType: number;
  locationId: number;
  localityId: number;

  location: any;
  locality: any;
  streetName: string;
  plotNo: string;

  currentUserAccount: any;

  cost: number;
  features: any[];

  rooms: any[];
  bathRooms: number;
  toilets: number;
  garages: number;
  bedRooms: number;
  Kitchens: number;
  SittingRooms: number;

}
