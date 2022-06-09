import {IInstructionRequest} from "./interface/instruction-request";
import {User} from "../../../../auth/models/user";
import {Locality} from "../../../models/locality";
import {Location} from "../../../models/location";

export class InstructionRequest implements IInstructionRequest {
  locationId = 0;
  locationName = "";
  location = new Location();

  localityId = 0;
  localityName = "";
  locality = new Locality();

  plotNumber = "";

  status = 0;
  appointmentStatus = 0;

  preferredAccessDate = "";
  accessContactName = "";
  accessContactMobileNumber = "";
  clientName = "";
  clientMobileNumber = "";

  CanBeReIssued = false;
  comment = "";

}
