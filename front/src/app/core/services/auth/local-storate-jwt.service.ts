import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocalStorateJwtService {
  private readonly tokenKey: string = 'jwtToken';
  private readonly refreshTokenKey: string = 'refreshToken';
  constructor() { }
  
  public getToken(): Observable<string | null> {
    return this.get(this.tokenKey);
  }

  public setToken(data: string): Observable<string> {
    return this.set(data, this.tokenKey);
  }

  
  public setRefreshToken(data: string): Observable<string> {
    localStorage.setItem(this.refreshTokenKey, data);
    return of(data);
  }
  
  public removeToken(): Observable<boolean> {
    localStorage.removeItem(this.tokenKey);
    return of(true);
  }

  private set(data: string, key: string): Observable<string> {
    localStorage.setItem(key, data);
    return of(data);
  }

  private get(key:string): Observable<string | null>{
    const data = localStorage.getItem(key);
    if (data) {
      return of(data);
    }
    return of(null);
  }
}
