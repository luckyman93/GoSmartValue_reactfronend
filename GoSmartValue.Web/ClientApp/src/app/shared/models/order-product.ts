import { ILocation } from "./interfaces/location";
import { IOrderProduct } from "./interfaces/order-report";

export class OrderProduct implements IOrderProduct {
    
    location: ILocation = {
        id:0,
        name:'',
        localities :null,
        verified : false
    }

    locality = {
        id:0,
        name:"",
        locationId: 33,
        verified:false,
        streets: null
    }
    date =  new Date();
    email=  '';
}