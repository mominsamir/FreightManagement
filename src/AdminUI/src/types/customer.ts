import { jsonToLocation, Location } from "./location";

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
    locations : Location[];
  }
  

  export const jsonToCustomer = (json: any) => Object.assign({}, json, {
      street: json.billingAddress.street,
      city: json.billingAddress.city,
      state: json.billingAddress.state,
      country: json.billingAddress.country,
      zipCode: json.billingAddress.zipCode,
      locations : json.locations === undefined ? [] : json.locations.map( (location: any) => jsonToLocation(location))
    });

   