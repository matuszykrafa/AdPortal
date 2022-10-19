import { OfferStatus } from "../enums/offer-status.enum";

export interface OfferModel {
    id: number,
    title: string,
    description: string,
    contactNumber: number,
    categoryId: number,
    authorId: number,
    isActive: boolean,
    offerStatus: OfferStatus
  }