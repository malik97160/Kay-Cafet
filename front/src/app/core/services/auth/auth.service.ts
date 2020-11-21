import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { StorageService } from '../storage/storage.service';
import { UserManager } from 'oidc-client'
import { ApplicationPaths, ApplicationName } from './api-authentication.constants';
import { BehaviorSubject } from 'rxjs';
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
  constructor(private http: HttpClient, private storageService: StorageService) {
  }

  isAuthenticated(): boolean {
    return false;
  }

  async signIn(){
    await this.ensureUserManagerInitialized();
    await this.userManager.signinRedirect();
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
      debugger;
      await this.userManager.removeUser();
      this.userSubject.next(null);
    });
  }
}
