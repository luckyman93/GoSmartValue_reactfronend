import {Locality} from "../../../../models/locality";
import {User} from "../../../../../auth/models/user";
import {Location} from "../../../../models/location";

export interface IInstructionRequest {
  locationId: number;
  locationName: string;
  location: Location;

  localityId: number;
  localityName: string;
  locality: Locality;

  plotNumber: string;

  status: number;
  appointmentStatus: number;

  preferredAccessDate: string;
  accessContactName: string;
  accessContactMobileNumber: string;
  clientName: string;
  clientMobileNumber: string;

  CanBeReIssued: boolean;
  comment: string;

}
