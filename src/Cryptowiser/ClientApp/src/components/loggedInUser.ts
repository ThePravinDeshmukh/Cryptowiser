import { IUser } from "../datasources/IUser";

export function LoggerInUser(): IUser
{
    if (localStorage.length == 0) return {} as IUser;

    if (localStorage.getItem("user") === null) return {} as IUser;
  
    var user: IUser = JSON.parse(localStorage.getItem('user') ?? "");

    return user;
}