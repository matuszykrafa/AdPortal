import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { OfferStatus } from '../enums/offer-status.enum';
import { OfferModel } from '../models/offer.model';
import { OfferStatusModel } from '../models/offer.status.model';

@Injectable({
  providedIn: 'root'
})
export class OfferService {

  constructor(private http: HttpClient) { }

  private get controllerUrl(): string {
    return environment.apiUrl + "/offer";
  }

  public getOfferByIdManagement(offerId: number): Observable<OfferModel> {
    return this.http.get<OfferModel>(`${this.controllerUrl}/GetOfferByIdManagement?offerId=${offerId}`)
  }
  public getOffersForUser(): Observable<OfferModel[]> {
    return this.http.get<OfferModel[]>(`${this.controllerUrl}/getOffersForUser`)
  }
  public addOrUpdateOffer(offerModel: OfferModel): Observable<OfferModel> {
    return this.http.post<OfferModel>(`${this.controllerUrl}/addOrUpdateOffer`, offerModel)
  }
  public changeStatus(offerId: number, status: OfferStatus): Observable<boolean> {
    return this.http.post<boolean>(`${this.controllerUrl}/changeOfferStatus`, {offerId: offerId, offerStatus: status} as OfferStatusModel)
  }

  public getOfferById(offerId: number): Observable<OfferModel> {
    return this.http.get<OfferModel>(`${this.controllerUrl}/getOfferById?offerId=${offerId}`)
  }

  public getOffersAdmin(): Observable<OfferModel[]> {
    return this.http.get<OfferModel[]>(`${this.controllerUrl}/getOffersAdmin`)
  }

  public getOffers(): Observable<OfferModel[]> {
    return this.http.get<OfferModel[]>(`${this.controllerUrl}/getOffers`)
  }

  public getOffersByCategory(categoryId: number): Observable<OfferModel[]> {
    return this.http.get<OfferModel[]>(`${this.controllerUrl}/getOffersByCategory?categoryId=${categoryId}`)
  }
}
