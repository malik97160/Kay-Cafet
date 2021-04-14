import { Injectable } from '@angular/core';
import { StorageService } from '../storage/storage.service';
import { UserManager } from 'oidc-client'
import { Observable } from 'rxjs';
import { LoginPayload } from './login-paylaod';
import jwtDecode from 'jwt-decode';
import { ApiService } from '../api/api.service';
import { Tokens } from 'src/app/Interfaces/shared-interface';
export interface IUser {
  name: string;
}

@Injectable({
  providedIn: 'root'
})


export class AuthService {
  clientId: string = "KayCafet";
  jwtKey: string = 'jwt';
  userManager: UserManager;
  constructor(private apiService: ApiService, private storageService: StorageService) {
  }

  isAuthenticated(): boolean {
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
    const decoded = jwtDecode(token) as JWtToken;

    if (decoded.exp === undefined) return null;

    const date = new Date(0); 
    date.setUTCSeconds(decoded.exp);
    return date;
  }
  
  getToken(): string {
      return this.storageService.retrieve(this.jwtKey);
  }

  login(credentials: LoginPayload): Observable<Tokens> {
    return this.apiService.post<Tokens, LoginPayload>('authentication/login', credentials);
  }
  
}

export class JWtToken{
  exp: number
}
