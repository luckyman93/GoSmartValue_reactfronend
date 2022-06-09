import {ILocality} from "./interfaces/locality";

export class Locality implements ILocality{
  id:number=0;
  name:string= "";
  locationId:number= 33;
  verified=false;
  streets=null;
}
