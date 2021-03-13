
export interface  Vendor {
    id: number;
    name : string;
    email : string;
    street : string;
    city: string;
    state: string;
    country: string;
    zipCode: string;
  } 


  export const jsonToVendor = (json: any) => {
    return Object.assign({}, json, {
      street: json.address.street,
      city: json.address.city,
      state: json.address.state,
      country: json.address.country,
      zipCode: json.address.zipCode,
  })
  };