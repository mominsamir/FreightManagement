import { Moment } from "moment";
import { jsonToUser, User } from "./user";
import { jsonToTrailer, jsonToTruck, Trailer, Truck } from "./vehicle";

export interface  DriverSchedule {
    id: number;
    name : string;
    startTime : Moment;
    endTime : Moment;
    driver : User;
    trailer: Trailer;
    truck: Truck;
    status: 'SCHEDULE_CREATED'| 'CHECKLIST_COMPLETE'|'SCHEDULE_COMPLETED';
    checkList: any[];    
  }


  export const jsonToDriverSchedule = (json: any) => Object.assign({}, json, {
        startTime: json.address.street,
        endTime: json.address.city,
        driver: jsonToUser(json.driver),
        trailer: jsonToTrailer(json.trailer),
        truck: jsonToTruck(json.truck),
    });

    export const jsonToLocationTanks = (json: any) => Object.assign({}, json, {});    



   