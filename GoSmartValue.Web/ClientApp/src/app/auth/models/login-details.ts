import { ILoginDetails } from "./interfaces/login-details";

export class LoginDetails implements ILoginDetails{
    token: string='';
    username: string='';
}