import {IInstantValuationReportRequest} from "./interface/instant-valuation";
import {Locality} from "../../../models/locality";
import {Location} from "../../../models/location";
import {features} from "../../../constants/constants";

export class InstantValuationReportRequest implements IInstantValuationReportRequest {
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
  cost = 0;
  features = features;
  rooms = [];
  bathRooms = 0;
  toilets = 0;
  garages = 0;
  bedRooms = 0;
  Kitchens = 0;
  SittingRooms = 0;
}
