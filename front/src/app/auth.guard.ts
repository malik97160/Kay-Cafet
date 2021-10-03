import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './core/services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

constructor(private _router: Router, private _authService: AuthService) {
}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    if (this._authService.isAuthenticated()){
      return true;
    }else{
      this._authService.redirectUrl = state.url;
      this._router.navigate(["login"]);
      return false;
    }
  }
  
}
