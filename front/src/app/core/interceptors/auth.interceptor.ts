import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from "@angular/core";
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(@Inject('BASE_API_URL') private baseUrl: string) {  }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
    debugger;
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiJkYzJkMTc4Ny1kYmRjLTRiYTAtOTA4My04YTE5NTM5ZTc3ZTUiLCJ1c2VyTmFtZSI6Im1hbGlrY291Y2h5IiwibmJmIjoxNjA3NzI1MDI3LCJleHAiOjE2MDc3MjUwNTcsImlhdCI6MTYwNzcyNTAyN30.dBR6VPxmwU90c30ZOY8xpydNYH7pTrKSKlSXiYh_wB0";
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
    return next.handle(apiReq);
    }
}