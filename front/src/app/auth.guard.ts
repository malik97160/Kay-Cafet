import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { map, switchMap, tap } from 'rxjs/operators';
import { AuthService } from './core/services/auth/auth.service';
import { LocalStorateJwtService } from './core/services/auth/local-storate-jwt.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

constructor(
  private _router: Router,
  private _authService: AuthService,
  private jwtService: LocalStorateJwtService) {
}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
    if (this._authService.isAuthenticated()){
      return true;
    }else{
      this._authService.redirectUrl = state.url;
      this._authService.generateRefreshToken().subscribe((tokens) => {
        this.jwtService.setRefreshToken(tokens.refreshToken);
        this.jwtService.setToken(tokens.jwtToken);
        this._router.navigate([state.url]);
        return true;
      },
      (error) => {
        this._router.navigate(["login"]);
        return false;
      });   
    }
  }
  
}
