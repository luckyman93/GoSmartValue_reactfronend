import { IPropertyDetail } from "./interfaces/property-detail";

export class PropertyDetails implements IPropertyDetail {

    constructor(public name: string,public numberOfRooms:number, public id: number) {}
}