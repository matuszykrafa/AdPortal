import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { OfferDetailsComponent } from './components/offers/offer-details/offer-details.component';
import { OfferListManagementComponent } from './components/offers/offer-list-management/offer-list-management.component';
import { OfferManagementAdminComponent } from './components/offers/offer-management-admin/offer-management-admin.component';
import { OfferManagementComponent } from './components/offers/offer-management/offer-management.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'offers/:categoryId', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'offer-details/:id', component: OfferDetailsComponent },
  { path: 'offer-management/:id', component: OfferManagementComponent },
  { path: 'offer-list-management', component: OfferListManagementComponent },
  { path: 'offer-list-management-admin', component: OfferManagementAdminComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
