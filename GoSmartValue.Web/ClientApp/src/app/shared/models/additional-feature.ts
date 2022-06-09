import { IAdditionalFeature } from "./interfaces/additional-feature";
import { IPropertyFeature } from "./interfaces/property-feature";

export class AdditionalFeature implements IAdditionalFeature{
    constructor(public features:IPropertyFeature[],public additionalInfo:string,public isValid?:boolean){}
}