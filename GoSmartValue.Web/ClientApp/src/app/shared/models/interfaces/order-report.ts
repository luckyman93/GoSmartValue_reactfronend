import { ILocality } from "./locality";
import { ILocation } from "./location";

export interface IOrderProduct {
    location: ILocation
    locality: ILocality
    date: Date
    email: string
}