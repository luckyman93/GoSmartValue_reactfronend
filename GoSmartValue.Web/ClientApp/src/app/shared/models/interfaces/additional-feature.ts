import { IPropertyFeature } from "./property-feature";

export interface IAdditionalFeature{
    features:IPropertyFeature[];
    additionalInfo:string;
    isValid?:boolean;
}