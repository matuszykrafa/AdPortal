import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/login.model';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { Observable } from 'rxjs';
import { TokenService } from './token.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { RegisterModel } from '../models/register.model';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private tokenService: TokenService, private jwtHelper: JwtHelperService) { }

  private get controllerUrl(): string {
    return environment.apiUrl + "/user";
  }

  public get isUserAuthenticated(): boolean {
    var token = this.tokenService.getToken();

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    return false;
  }

  public get currentUserName(): string | null {
    var token = this.tokenService.getToken();
    
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const tokenInfo = this.jwtHelper.decodeToken(token);
      return tokenInfo['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']
    }
    return null;
  }
  public get currentUserRole(): string | null {
    var token = this.tokenService.getToken();
    
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      const tokenInfo = this.jwtHelper.decodeToken(token);
      return tokenInfo['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
    }
    return null;
  }

  public login(loginModel: LoginModel): Observable<AuthenticatedResponse> {
    return this.http.post<AuthenticatedResponse>(`${this.controllerUrl}/login`, loginModel)
  }

  public register(registerModel: RegisterModel): Observable<AuthenticatedResponse> {
    return this.http.post<AuthenticatedResponse>(`${this.controllerUrl}/register`, registerModel)
  }

  public logout(): void {
    this.tokenService.clearToken();
  }

  public getUserData(): Observable<RegisterModel> {
    return this.http.get<RegisterModel>(`${this.controllerUrl}/getUserData`)
  }
}
