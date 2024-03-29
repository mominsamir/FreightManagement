
export interface  Rack {
    id : number;
    name : string;
    irsCode : string;
    street : string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
  } 


  
export const jsonToRack = (json: any) => {
  return Object.assign({}, json, {
    street: json.address === undefined? '' : json.address.street ,
    city: json.address === undefined? '' :json.address.city,
    state: json.address === undefined? '' :json.address.state,
    country: json.address === undefined? '' :json.address.country,
    zipCode: json.address === undefined? '' :json.address.zipCode,
})
};