import {Locality} from "../../../models/locality";
import {Location} from "../../../models/location";
import {features} from "../../../constants/constants";
import { IDetailedValuationReportRequest } from "./interface/detailed-valuation";

export class DetailedValuationReportRequest implements IDetailedValuationReportRequest {
  metric = 1;
  plotSize = 0;
  landUse = 1;
  propertyType = 1;
  locationId = 0;
  localityId = 0;
  
  location = new Location();
  locality = new Locality();
  streetName = '';
  plotNo = '';
  currentUserAccount = null;


  valuer = '';
  clientName = '';
  cellNumber = '';
  accessDate = new Date();
}
