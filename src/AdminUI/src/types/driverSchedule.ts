import moment from "moment";
import { Moment } from "moment-timezone";
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
    status: number;
    checkList: any[];    
  }


  export const jsonToDriverSchedule = (json: any) => Object.assign({}, json, {
        startTime: json.address.street,
        endTime: json.address.city,
        driver: jsonToUser(json.driver),
        trailer: jsonToTrailer(json.trailer),
        truck: jsonToTruck(json.truck),
    });

//    export const jsonToLocationTanks = (json: any) => Object.assign({}, json, {});    


    export interface  DriverScheduleList {
      id: number;
      name : string;
      startTime : moment.Moment;
      endTime : moment.Moment;
      driver : User;
      trailer: Trailer;
      truck: Truck;
      status: 'SCHEDULE_CREATED'| 'CHECKLIST_COMPLETE'|'SCHEDULE_COMPLETED';
      checkList: any[];    
    }
  
    export const jsonToDriverScheduleList = (json: any) => {
        console.log();
        return Object.assign({}, json, { 
          startTime: moment(json.startTime),
          endTime: moment(json.startTime),
          driver: jsonToUser(json.driver),
          trailer: jsonToTrailer(json.trailer),
          truck: jsonToTruck(json.truck)
    });
    };


    export interface  DriverScheduleCreate {
      startTime : Moment;
      driverId: number;
      trailerId : number;
      truckId: number;      
    }


    
  export const scheduleStatusMap : Record<string, string> = {
    "0": 'Scheduled',
    "1": 'Checked In',
    "2": 'Completed'       
 }

