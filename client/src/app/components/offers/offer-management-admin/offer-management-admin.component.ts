import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { OfferStatus } from 'src/app/enums/offer-status.enum';
import { OfferModel } from 'src/app/models/offer.model';
import { OfferService } from 'src/app/services/offer.service';

@Component({
  selector: 'app-offer-management-admin',
  templateUrl: './offer-management-admin.component.html',
  styleUrls: ['./offer-management-admin.component.css']
})
export class OfferManagementAdminComponent implements OnInit {
  offers: OfferModel[] = [];
  statusEnum: typeof OfferStatus = OfferStatus;

  dataSource: MatTableDataSource<OfferModel> = new MatTableDataSource<OfferModel>([]);
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(
    private offerService: OfferService,
    private router: Router) {

  }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(): void {
    this.offerService.getOffersAdmin().subscribe(res => {
      this.offers = res;
      this.dataSource.data = this.offers
    })
  }

  displayedColumns: string[] = ['title', 'offerStatus', 'action'];


  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getOfferStatus(offerStatus: OfferStatus) {
    return OfferStatus[offerStatus]
  }
  navigateToDetails(id: number): void {
    this.router.navigate([`/offer-details/${id}`])
  }

  changeStatus(id: number, offerStatus: OfferStatus = OfferStatus.CANCELLED): void {
    this.offerService.changeStatus(id, offerStatus).subscribe({
      next: res => {
        var changedOffer = this.offers.find(x => x.id == id)
        if (changedOffer != undefined)
          changedOffer.offerStatus = offerStatus;
      }
    })
  }

  get cancelledStatus(): OfferStatus {
    return OfferStatus.CANCELLED;
  }
}
