
  export interface  FuelProduct {
    id: number;
    name : string;
    grade : number;
    uom : number;
  }  


  export const jsonToFuelProduct = (json: any) => Object.assign({}, json, {});  