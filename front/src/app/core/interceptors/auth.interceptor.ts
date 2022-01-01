import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { Tokens } from 'src/app/Interfaces/shared-interface';
import { AuthService } from '../services/auth/auth.service';
import { LocalStorateJwtService } from '../services/auth/local-storate-jwt.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private isRefreshing: boolean = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject(null);
    constructor(
      @Inject('BASE_API_URL') private baseUrl: string,
      private readonly authService: AuthService,
      private readonly jwtService: LocalStorateJwtService
    ) {  }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    const token = this.authService.token;
    const headerSettings: {[name: string]: string | string[]; } = {};

    for (const key of request.headers.keys()) {
      headerSettings[key] = request.headers.getAll(key);
    }
    if (token) {
      headerSettings['Authorization'] = 'Bearer ' + token;
    }
    headerSettings['Content-Type'] = 'application/json';
    const newHeader = new HttpHeaders(headerSettings);

    const apiReq = request.clone({url: `${this.baseUrl}/${request.url}`, headers: newHeader});
    return next.handle(apiReq).pipe(
      catchError(error => {
        if(this.is401Error(error, apiReq.url)){
          return this.handle401Error(request, next);
        }

        throw Error(JSON.stringify(error));
      })
    );
    }

    private is401Error(error, url: string): boolean{
      return error instanceof HttpErrorResponse && !url.includes('authentication/login') && !url.includes('authentication/refreshToken');
    }

    private handle401Error(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
      if(!this.isRefreshing){
        this.isRefreshing = true;
        this.refreshTokenSubject.next(null);

        const refreshToken = this.jwtService.getRefreshToken();
        if(refreshToken){
          return this.authService.generateRefreshToken().pipe(
            switchMap((tokens: Tokens) => {
              this.isRefreshing = false;
              this.jwtService.setRefreshToken(tokens.refreshToken);
              this.refreshTokenSubject.next(tokens.refreshToken);
              return next.handle(this.addTokenHeader(request, tokens.refreshToken));
            }
          ));
        }
      }

      return this.refreshTokenSubject.pipe(
        filter(token => token !== null),
        take(1),
        switchMap((token) => next.handle(this.addTokenHeader(request, token)))
      );
      
    }

    private addTokenHeader(request: HttpRequest<any>, token: string){
      return request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
    }
}