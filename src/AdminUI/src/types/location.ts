
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
    fuelGrade : 'REGULAR'| 'PLUS'| 'SUPER'|'DIESEL_CLR' |'DIESEL_DYD';
    capactity : number;
  }


  export const jsonToLocation = (json: any) => Object.assign({}, json, {
      street: json.address.street,
      city: json.address.city,
      state: json.address.state,
      country: json.address.country,
      zipCode: json.address.zipCode,
      tanks: json.tanks.map((tanks: any)=> jsonToLocationTanks(tanks)),
    });

    export const jsonToLocationTanks = (json: any) => Object.assign({}, json, {});    
   