import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginPayload } from './login-paylaod';
import { map } from 'rxjs/operators'
import { StorageService } from '../storage/storage.service';
import { JwtHelperService } from '@auth0/angular-jwt/lib/jwthelper.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl: any;
  clientId: string = "KayCafet";
  jwtKey: string = 'jwt';
  constructor(private jwtHelper: JwtHelperService, private http: HttpClient, private storageService: StorageService) { 
    this.baseUrl = environment.apiUrl;
  }

  isAuthenticated(): boolean {
    debugger;
    const token = this.storageService.retrieve(this.jwtKey);
    return !this.jwtHelper.isTokenExpired(token);
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
