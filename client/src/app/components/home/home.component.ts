import { AfterViewInit, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryModel } from 'src/app/models/category.model';
import { OfferModel } from 'src/app/models/offer.model';
import { CategoryService } from 'src/app/services/category.service';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, AfterViewInit {
  categories: CategoryModel[] = [];
  offers: OfferModel[] = [];
  categoryId: number;
  constructor(
    private categoryService: CategoryService,
    private offerService: OfferService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    this.activatedRoute.params.subscribe(param => {
      if (param?.categoryId != null && param?.categoryId > 0) {
        this.categoryId = Number(param?.categoryId);
        this.selectCategory(this.categoryId)
      } else {
        this.selectCategory(1);
      }
    });
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {

    this.fetchData();
  }

  fetchData(): void {
    this.categoryService.getCategories().subscribe({
      next: res => {
        this.categories = res;
        if (this.categoryId > 0)
          this.selectCategory(this.categoryId)
      }
    })
  }

  selectCategory(categoryId: number) {
    this.offerService.getOffersByCategory(categoryId).subscribe({
      next: res => {
        this.offers = res;  
      }
    })
  }
  categorySelcted(categoryId: number) {
    this.router.navigate([`/offers/${categoryId}`])
  }

  goToDetails(off: OfferModel): void {
    this.router.navigate([`offer-details/${off.id}`])
  }

  public getDesc(desc: string): string {
    const lengthMax = 75;
    if (desc.length < lengthMax) return desc;
    var res = desc.slice(0,lengthMax).trimEnd() + '...';
    return res;
  }
}
