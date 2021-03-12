import { jsonToLocation } from "./location";

export interface  Customer {
    id: number;
    name : string;
    email : string;
    isActive : boolean;
    street : string;
    city: string;
    state: string;
    country: string;
    zipCode: string;    
    Locations : Location[];
  }
  

  export const jsonToCustomer = (json: any) => Object.assign({}, json, {
      street: json.address.street,
      city: json.address.city,
      state: json.address.state,
      country: json.address.country,
      zipCode: json.address.zipCode,
      tanks: json.location.map( (location: any) => jsonToLocation(location)),
    });

   