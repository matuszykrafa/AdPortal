import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { JwtModule } from "@auth0/angular-jwt";
import { TokenService } from './services/token.service';
import { environment } from 'src/environments/environment';
import { HomeComponent } from './components/home/home.component';
import { AlertComponent } from './components/alert/alert.component';
import { RegisterComponent } from './components/register/register.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { LogoutComponent } from './components/logout/logout.component';
import { OfferManagementComponent } from './components/offers/offer-management/offer-management.component';
import { OfferDetailsComponent } from './components/offers/offer-details/offer-details.component';
import { OfferListManagementComponent } from './components/offers/offer-list-management/offer-list-management.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule  } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { OfferManagementAdminComponent } from './components/offers/offer-management-admin/offer-management-admin.component';
import { MatDividerModule } from '@angular/material/divider';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    AlertComponent,
    RegisterComponent,
    NavbarComponent,
    LogoutComponent,
    OfferManagementComponent,
    OfferDetailsComponent,
    OfferListManagementComponent,
    OfferManagementAdminComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatSortModule,
    MatToolbarModule,
    MatButtonModule,
    MatDialogModule,
    MatPaginatorModule,
    MatDividerModule,
    MatInputModule,
    MatTableModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: TokenService.getToken,
        allowedDomains: [environment.allowedDomains],
        disallowedRoutes: []
      }
    }),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
