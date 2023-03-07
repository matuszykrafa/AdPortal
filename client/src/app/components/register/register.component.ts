import { HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login.model';
import { RegisterModel } from 'src/app/models/register.model';
import { AlertService } from 'src/app/services/alert.service';
import { TokenService } from 'src/app/services/token.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string = "/login";

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
          username: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
          password: ['', [Validators.required]],
          confirmPassword: ['', [Validators.required]],
          phoneNumber: ['', [Validators.required, Validators.pattern('[- +()0-9]+')]],
          email: ['', [Validators.required] ]
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
      var credentials: RegisterModel = {
        username: this.f.username.value,
        password: this.f.password.value,
        email: this.f.email.value,
        phoneNumber: this.f.phoneNumber.value ?? null
      }
      this.loading = true;this.userService.register(credentials).subscribe({
        next: (res) => {
          if (res)
          this.router.navigate(["/login"]);
          else {
            this.loading = false;
            this.alertService.error("Could not register: email or username already exists")
          }
        },
        error: (error: HttpErrorResponse) => {
          this.loading = false;
          this.alertService.error(error.message)
        } 
      })
  }
}