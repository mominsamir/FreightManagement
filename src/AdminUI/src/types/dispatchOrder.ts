import { DriverSchedule, jsonToDriverSchedule } from "./driverSchedule";
import { Moment } from "moment-timezone";
import moment from "moment";
import { jsonToRack, Rack } from "./rack";
import { jsonToOrderItem, OrderItem } from "./order";

export interface Dispatch {
    id:                number;
    driverSchedule:    DriverSchedule;
    dispatchDateTime:   Moment;
    status:            number;
    statusLabel:       string;    
    miles:             number;
    dispatchStartTime:  Moment;
    dispatchEndTime:    Moment;
    rackArrivalTime:    Moment;
    rackLeftOnTime:    Moment;
    loadingStartTime:  Moment;
    loadingEndTime:    Moment;
    dispatchLoadings:   DispatchLoading[];
}

export interface DispatchLoading {
    id:             number;
    rack:           Rack;
    orderItem:      OrderItem;
    billOfLoading:  string;
    loadedQuantity: number;
}

export const jsonToDispatch = (json: any) => Object.assign({}, json, {
    dispatchDateTime: moment(json.dispatchDateTime),
    dispatchStartTime: moment(json.dispatchStartTime),
    dispatchEndTime: moment(json.dispatchEndTime),
    rackArrivalTime: moment(json.rackArrivalTime),
    rackLeftOnTime: moment(json.rackLeftOnTime),
    loadingStartTime: moment(json.loadingStartTime),
    loadingEndTime: moment(json.loadingEndTime),        
    driverSchedule: jsonToDriverSchedule(json.driverSchedule),
    dispatchLoadings: json.dispatchLoadings === undefined ? [] :json.dispatchLoadings.map((j: any)  => jsonToDispatchLoading(j))
});

export const jsonToDispatchLoading = (json: any) => Object.assign({}, json, {
    billOfLoading : json.billOfLoading === null? '':  json.billOfLoading,
    rack: json.rack=== undefined ? {}: jsonToRack(json.rack),
    orderItem: json.orderItem=== undefined ? {}: jsonToOrderItem(json.orderItem)
});

