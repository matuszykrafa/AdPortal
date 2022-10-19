import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OfferStatus } from 'src/app/enums/offer-status.enum';
import { UserType } from 'src/app/enums/user-type.enum';
import { OfferModel } from 'src/app/models/offer.model';
import { AlertService } from 'src/app/services/alert.service';
import { OfferService } from 'src/app/services/offer.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-offer-details',
  templateUrl: './offer-details.component.html',
  styleUrls: ['./offer-details.component.css']
})
export class OfferDetailsComponent implements OnInit {
  offerId: number;
  offer: OfferModel;
  admin = false;
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private offerService: OfferService,
    private alertService: AlertService,
    private userService: UserService
  ) {

    this.activatedRoute.params.subscribe(param => {
      if (param?.id != null && param?.id > 0) {
        this.offerId = Number(param?.id);
      } else {
        this.router.navigate(["/"])
      }
    });
    
  }

  ngOnInit(): void {
    this.fetchData();
    var role = this.userService.currentUserRole;
    if (role == UserType[UserType.ADMIN]) {
      this.admin = true;
    }
  }

  fetchData(): void {
    this.offerService.getOfferById(this.offerId).subscribe({
      next: res => {
        this.offer = res;
        if (this.offer == null) {
          this.alertService.error("Nie znaleziono takiej oferty")
        }
      },
      error: (err: HttpErrorResponse) => {
        this.alertService.error(err.error?.title || err.message)
      }
    })
    
  }

  getStatus(offerStatus:OfferStatus) {
    return OfferStatus[offerStatus];
  }
}
