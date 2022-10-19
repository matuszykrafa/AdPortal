import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { AlertService } from 'src/app/services/alert.service';
import { TokenService } from 'src/app/services/token.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string = "/";

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private userService: UserService,
      private tokenService: TokenService,
      private alertService: AlertService 
  ) {
      if (this.userService.isUserAuthenticated) { 
          this.router.navigate(['/']);
      }
      this.loginForm = this.formBuilder.group({
          username: ['', Validators.required],
          password: ['', Validators.required]
      });
  }

  ngOnInit() {
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
      this.submitted = true;

      if (this.loginForm.invalid) {
          return;
      }
      var credentials: LoginModel = {
        username: this.f.username.value,
        password: this.f.password.value
      }
      this.loading = true;this.userService.login(credentials).subscribe({
        next: (res) => {
          this.tokenService.setToken(res);
          this.router.navigate(["/"]);
        },
        error: (error: HttpErrorResponse) => {
          this.loading = false;
          this.alertService.error(error.error.title || error.message)
        } 
      })
  }
}