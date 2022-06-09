import { IPropertyFeature } from "./interfaces/property-feature";


export class PropertyFeature implements IPropertyFeature {
    constructor( public name: string, public id: number, public isSelected:boolean){}
}
