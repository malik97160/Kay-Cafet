import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginPayload } from './login-paylaod';
import { map } from 'rxjs/operators'
import { StorageService } from '../storage/storage.service';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  clientId: string = "KayCafet";
  jwtKey: string = 'jwt';
  constructor(private http: HttpClient, private storageService: StorageService) {
  }

  isAuthenticated(): boolean {
    debugger;
    const token = this.storageService.retrieve(this.jwtKey);
    return !this.isTokenExpired(token);
  }

  isTokenExpired(token: string) {
    if(!token) return true;

    const date = this.getTokenExpirationDate(token);
    if(date === undefined) return false;
    return !(date.valueOf() > new Date().valueOf());
  }

  getTokenExpirationDate(token: string) {
    debugger;
    const decoded = jwtDecode(token) as JWtToken;

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }
  
  getToken(): string {
      return this.storageService.retrieve(this.jwtKey);
  }

  login(loginPayload: LoginPayload){
    const auth = {
      username: loginPayload.login,
      password: loginPayload.password,
      grant_type: 'password',
      client_id: this.clientId
    }

    debugger;
    const credentials = JSON.stringify(auth);
    this.http.post('/api/authentication/login', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(map(data => {})).subscribe(result => {
      const token = (<any>result).token;
      this.storageService.store(this.jwtKey, token);
    })
  }
}

export class JWtToken{
  exp: number
}
