export interface IDetailedValuationReportRequest {
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

  valuer: string;
  clientName: string;
  cellNumber: string;
  accessDate: Date;

}
