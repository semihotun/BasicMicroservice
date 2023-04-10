import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenLocalStorage {

  constructor() { }

  setToken(token: string) {
    localStorage.setItem("token", token);
  }

  removeToken() {
    localStorage.removeItem("token");
  }

  removeItem(itemName: string) {
    localStorage.removeItem(itemName);
  }

  getToken(): string {
    let result= localStorage.getItem("token");
    return result == null ? "" : result;
  }

  setItem(key: string, data: any) {
    localStorage.setItem(key, data);
  }

  getItem(key: string) {
    return localStorage.getItem(key);
  }
}
