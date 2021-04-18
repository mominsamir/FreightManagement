import moment from "moment";
import { Customer, jsonToCustomer } from "./customer";
import { Location,jsonToLocation } from "./location";
import { FuelProduct, jsonToFuelProduct } from "./product";


export interface  Order {
    id: number;
    orderDate : moment.Moment;
    shipDate : moment.Moment;
    status : number;
    statusLabel : string;
    customer: Customer;
    totalQnt: number;
    orderItems: OrderItem[]; 
}  

export interface  OrderItem {
    id: number;
    location : Location;
    fuelProduct : FuelProduct;
    quantity : number;
    loadCode: string;
}  

  export const jsonToOrder = (json: any) => Object.assign({}, json, {
    shipDate: moment(json.shipDate),
    orderDate: moment(json.orderDate),
    customer : jsonToCustomer(json.customer),
    orderItems: json.orderItems === undefined ?  [] : json.orderItems.map((orderItems: any) => jsonToOrderItem(orderItems))
  });

  export const jsonToOrderItem = (json: any) => Object.assign({}, json, {
    location : json.location === undefined ? {}: jsonToLocation(json.location),
    fuelProduct: json.fuelProduct === undefined ? {}:jsonToFuelProduct(json.fuelProduct)
  });    

  export const OrderStatusMap : Record<string, string> = {
    "0": 'Received',
    "1": 'Shipped',
    "2": 'Delivered',
    "3":"Cancelled"
  }


  export interface  OrderModel {
    id: number;
    orderDate : moment.Moment;
    shipDate : moment.Moment;
    customerId?: number;
    orderItems: OrderItemModel[]; 
}  

export interface  OrderItemModel {
    id: number;
    locationId : number;
    fuelProductId : number;
    quantity : number;
    loadCode: string;
} 

export const jsonToOrderUpdate = (json: any) => Object.assign({}, {}, {
  id: json.id,
  shipDate: json.shipDate,
  orderDate: json.orderDate,
  orderItems: json.orderItems === undefined ?  [] : json.orderItems.map((orderItems: any) => jsonToOrderItemUpdate(orderItems))
});

export const jsonToOrderItemUpdate = (json: any) => Object.assign({}, {}, {
  id: json.id,
  quantity: json.quantity,
  loadCode: json.loadCode,
  locationId : json.location.id,
  fuelProductId: json.fuelProduct.id
});