export interface UserIdendity {
    email: string;
    password: string;
}

export interface ChangePassword {
    id:       number;    
    password: string; 
    confirmPassword: string;
}
 



export interface User {
    id:       number;
    firstName: string;
    lastName:  string;
    email:    string;
    role:      string;
    isActive: boolean;
}



export const jsonToUser = (json: any) => Object.assign({}, json, {});