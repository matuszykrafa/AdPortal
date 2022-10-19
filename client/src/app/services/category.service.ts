import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoryModel } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }

  private get controllerUrl(): string {
    return environment.apiUrl + "/category";
  }
  public getCategories(): Observable<CategoryModel[]> {
    return this.http.get<CategoryModel[]>(`${this.controllerUrl}/getCategories`)
  }
}
