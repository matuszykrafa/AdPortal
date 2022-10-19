import { Injectable } from '@angular/core';
import { AuthenticatedResponse } from '../models/authenticated-response.model';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  private static tokenFieldName: string = "jwt";
  constructor() { }

  public setToken(authResponse: AuthenticatedResponse): void {
    localStorage.setItem(TokenService.tokenFieldName, authResponse.token);
  }

  public clearToken(): void {
    localStorage.removeItem(TokenService.tokenFieldName);
  }
  public getToken(): string | null {
    return TokenService.getToken(); 
  }

  public static getToken(): string | null {
    return localStorage.getItem("jwt"); 
  }
}
