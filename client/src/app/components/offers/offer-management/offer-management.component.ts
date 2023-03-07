import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { OfferStatus } from 'src/app/enums/offer-status.enum';
import { CategoryModel } from 'src/app/models/category.model';
import { OfferModel } from 'src/app/models/offer.model';
import { RegisterModel } from 'src/app/models/register.model';
import { AlertService } from 'src/app/services/alert.service';
import { CategoryService } from 'src/app/services/category.service';
import { OfferService } from 'src/app/services/offer.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-offer-management',
  templateUrl: './offer-management.component.html',
  styleUrls: ['./offer-management.component.css']
})
export class OfferManagementComponent implements OnInit {
  offer: OfferModel;
  private offerId: number;
  userData: RegisterModel;
  categories: CategoryModel[] = [];
  offerForm: FormGroup;
  loading = false;
  submitted = false;
  access = false;
  returnUrl: string = "/";
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private offerService: OfferService,
    private alertService: AlertService,
    private formBuilder: FormBuilder,
    private userService: UserService,
    private categoryService: CategoryService) {

    this.activatedRoute.params.subscribe(param => {
      if (param?.id != null && param?.id > 0) {
        this.offerId = Number(param?.id);
      } else {
        this.offerId = 0;
        this.access = true;
      }
    });

    this.setupForm();
  }

  ngOnInit(): void {
    this.fetchData();
  }

  setupForm(): void {
    this.offerForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required, Validators.min(0), Validators.max(999999)],
      phoneNumber: ['', Validators.required],
      categoryId: ['', [Validators.required]]
    });
  }

  fetchData(): void {
    this.userService.getUserData().subscribe({
      next: (res) => {
        this.userData = res;
        this.f.phoneNumber.setValue(this.userData.phoneNumber, { emitEvent: false });
        this.fetchOffer();
        this.fetchCategories();
      },
      error: (err: HttpErrorResponse) => {
        this.alertService.error(err.error?.title || err.message)
      }
    });
  }

  fetchOffer(): void {
    if (this.offerId > 0)
      this.offerService.getOfferByIdManagement(this.offerId).subscribe({
        next: (res) => {
          this.offer = res;
          this.setupValues();
          this.access = true;
        },
        error: (err: HttpErrorResponse) => {
          this.alertService.error(err?.error?.title || err.message);
          this.access = false;
        }
      })
  }
  fetchCategories(): void {
    this.categoryService.getCategories().subscribe(res => {
      this.categories = res;
    })
  }

  setupValues(): void {
    this.offerForm.setValue({
      title: this.offer.title,
      description: this.offer.description,
      price: this.offer.price,
      categoryId: this.offer.categoryId,
      phoneNumber: this.userData.phoneNumber,
    }, { emitEvent: false })
  }

  get f() { return this.offerForm.controls; }

  onSubmit() {
    this.submitted = true;

    if (this.offerForm.invalid) {
      console.log(this.f.categoryId, this.submitted, this.f.categoryId.errors)
      return;
    }

    var offerModel = this.prepareOfferModel();
    this.offerService.addOrUpdateOffer(offerModel).subscribe({
      next: (res) => {
        if (res) {
          this.router.navigate([`/offer-list-management`])
        } else {
          this.alertService.error("Coś poszło nie tak")
        }
      },
      error: (err) => {
        this.alertService.error(err.error?.title || err.message)
      }
    })
  }

  prepareOfferModel(): OfferModel {
    var offerModel: OfferModel = {
      id: this.offer ? this.offer.id : 0,
      title: this.f.title.value,
      description: this.f.description.value,
      price: this.f.price.value,
      contactNumber: this.f.phoneNumber.value,
      categoryId: this.f.categoryId.value,
      authorId: this.offer ? this.offer.authorId : 0,
      isActive: true,
      offerStatus: OfferStatus.PENDING
    }
    return offerModel;
  }
}
