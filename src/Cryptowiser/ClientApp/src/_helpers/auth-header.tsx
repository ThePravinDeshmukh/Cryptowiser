import { IUser } from "../datasources/IUser";

export function authHeader() {
    // return authorization header with jwt token
    let user : IUser = getUser();

    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}

function getUser() {
  let user01 = {} as IUser;
  if (localStorage.getItem("user") === null) {
  } else {
    user01 = JSON.parse(localStorage.getItem('user') ?? "");
  }
  return user01;
}