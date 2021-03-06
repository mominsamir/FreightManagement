import { Menu } from "components/Menu/menu";
import { User } from "./user";

export interface ConfigState {
  loaded: boolean;
  user : User;
  menus: Menu[];  
}


export interface PaymentTerm {
  uid: number;
  name: string;
  description: string;
  code: string;
  status: string;
}


export interface EntityResponse {
  uid : number;
  error: number;
  url?: string;  
}

export interface ChangePassword {
  oldPassword : string;
  password: string;
  confirmPassword?: string;  
}