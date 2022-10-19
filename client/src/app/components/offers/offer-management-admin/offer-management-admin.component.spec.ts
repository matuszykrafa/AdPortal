import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OfferManagementAdminComponent } from './offer-management-admin.component';

describe('OfferManagementAdminComponent', () => {
  let component: OfferManagementAdminComponent;
  let fixture: ComponentFixture<OfferManagementAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OfferManagementAdminComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OfferManagementAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
