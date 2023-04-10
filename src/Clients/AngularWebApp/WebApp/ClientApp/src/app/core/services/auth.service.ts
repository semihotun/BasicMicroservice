import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserForLoginDto } from '../models/identityService/userForLoginDto';
import { environment } from 'src/environments/environment';
import { AccessToken } from '../models/identityService/accessToken';
import { TokenLocalStorage } from './token-local-storage.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private httpClient: HttpClient,
    private tokenLocalStorage: TokenLocalStorage
    ) { }

  login(userForLoginDto: UserForLoginDto): Observable<AccessToken> {
    let result = this.httpClient.post<AccessToken>(environment.getAuthUrl + 'Auth/login', userForLoginDto);

    return result;
  }

  Islogged(): boolean {
    if (this.tokenLocalStorage.getToken()) {
      return true;
    }
    return false;
  }

  logout(){
    this.tokenLocalStorage.removeToken();
  }

}
