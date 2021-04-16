
export interface  Location {
    id: number;
    name : string;
    email : string;
    isActive : boolean;
    street : string;
    city: string;
    state: string;
    country: string;
    zipCode: string;    
    tanks : LocationTank[];
  }
  
  export interface  LocationTank {
    id: number;
    name : string;
    fuelGrade : number;
    fuelGradeLabel: string;
    capactity : number;
  }


  export const jsonToLocation = (json: any) => Object.assign({}, json, {
      street: json.deliveryAddress.street,
      city: json.deliveryAddress.city,
      state: json.deliveryAddress.state,
      country: json.deliveryAddress.country,
      zipCode: json.deliveryAddress.zipCode,
      tanks: json.tanks === undefined ?  [] : json.tanks.map((tanks: any)=> jsonToLocationTanks(tanks)),
    });

    export const jsonToLocationTanks = (json: any) => Object.assign({}, json, {});    
   