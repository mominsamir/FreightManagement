import moment from "moment";


  export interface  Truck {
    id : number;    
    numberPlate : string;
    vin : number;
    nextMaintanceDate: moment.Moment;
    status: string;
  } 
  
  export interface  Trailer {
    id : number;
    numberPlate : string;
    vin : number;
    capacity : number;
    compartment: number;
    status: string;
  } 


  export const vehicleStatusMap : Record<string, string> = {
    "0": 'Active',
    "1": 'Under Maintance',
    "3": 'Out of Service'       
 }


 export const jsonToTruck = (json: any) => Object.assign({}, json, {
  nextMaintanceDate : moment(json.nextMaintanceDate)
 });

 export const jsonToTrailer = (json: any) => Object.assign({}, json, {});

