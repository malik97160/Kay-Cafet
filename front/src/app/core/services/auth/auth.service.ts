import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from '../storage/storage.service';
import { UserManager, UserManagerSettings } from 'oidc-client'
import { ApplicationPaths, ApplicationName } from './api-authentication.constants';
import { BehaviorSubject } from 'rxjs';
import { LoginPayload } from './login-paylaod';
import { map } from 'rxjs/operators'
import jwtDecode from 'jwt-decode';
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
  private userSubject: BehaviorSubject<IUser | null> = new BehaviorSubject(null);
  private manager = new UserManager(getClientSettings());
  constructor(private http: HttpClient, private storageService: StorageService) {
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

  async signIn(loginPayload: LoginPayload){
    this.http.get("api/authentication/testAuthentication").subscribe(data =>{
      debugger;
      console.log("data => "+data)
    })
    //return this.manager.signinRedirect();   
    /*const auth = {
      username: loginPayload.login,
      password: loginPayload.password,
      grant_type: 'password',
      client_id: this.clientId
    }

    debugger;
    const credentials = JSON.stringify(auth);
    await this.http.post('api/authentication/signIn', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).pipe(map(data => {})).subscribe(result => {
      const token = (<any>result).token;
      this.storageService.store(this.jwtKey, token);
    })*/
  }
  
}

export function getClientSettings(): UserManagerSettings {
  return {
      authority: 'http://localhost:5000',
      client_id: 'angular_spa',
      redirect_uri: 'http://localhost:4200/auth-callback',
      post_logout_redirect_uri: 'http://localhost:4200/',
      response_type:"id_token token",
      scope:"openid profile email api.read",
      filterProtocolClaims: true,
      loadUserInfo: true,
      automaticSilentRenew: true,
      silent_redirect_uri: 'http://localhost:4200/silent-refresh.html'
  };
}

export class JWtToken{
  exp: number
}

  /*async signIn(){
    await this.ensureUserManagerInitialized();
    await this.userManager.signinRedirect()
    .then(() =>{
      console.log("signInRedirect done");
    }).catch((err) =>{
      console.log(err);
    });
  }

  private async ensureUserManagerInitialized(): Promise<void> {
    if (this.userManager !== undefined) {
      return;
    }

    const response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
    if (!response.ok) {
      throw new Error(`Could not load settings for '${ApplicationName}'`);
    }

    const settings: any = await response.json();
    settings.automaticSilentRenew = true;
    settings.includeIdTokenInSilentRenew = true;
    this.userManager = new UserManager(settings);

    this.userManager.events.addUserSignedOut(async () => {
      await this.userManager.removeUser();
      this.userSubject.next(null);
    });
  }
}
*/