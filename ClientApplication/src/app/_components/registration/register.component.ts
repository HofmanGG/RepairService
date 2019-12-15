import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '@core/_services/authentication.service';
import { CountryService } from '@core/_services/country.service';
import { AlertService } from '@core/_services/alert.service';
import { MustMatch } from 'app/_validators/must-match.validator';
import { MustHaveDigit } from 'app/_validators/must-have-digit.validator';
import { MustHaveUniqueChar } from 'app/_validators/must-have-unique-char.validator';
import { MustHaveNonAlphanumeric } from 'app/_validators/must-have-non-alphanumeric.validator';
import { SelectComponent } from '@shared/select/select.component';
import { DateMustBeValid } from 'app/_validators/date-must-be-valid.validator';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  loading: boolean;
  submitted: boolean;

  sc: StatusCodes = new StatusCodes();

  genders: Array<string>;

  days: Array<number>;
  months: Array<number>;
  years: Array<number>;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private authService: AuthenticationService,
    private alertService: AlertService,
    private countryService: CountryService,
    private statusCodeSvc: StatusCodeService
    ) {
      // redirect to home if already logged in
      if (this.authenticationService.currentUserValue) {
        this.router.navigate(['/']);
      }

      this.genders = ['Male', 'Female'];

      this.days = Array(31).fill(0).map((x, i) => i + 1);
      this.months = Array(12).fill(0).map((x, i) => i + 1);
      this.years = Array(94).fill(0).map((x, i) => -i + 2013);
    }

  ngOnInit() {
    this.registerForm = this.fb.group({
      firstName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20)]
      ],
      lastName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20)]
      ],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      gender: ['', Validators.required],
      dayOfBirth: ['', Validators.required],
      monthOfBirth: ['', Validators.required],
      yearOfBirth: ['', Validators.required],
      countryId: ['', Validators.required],
    }, {
      validators: [
        MustMatch('password', 'confirmPassword'),
        MustHaveDigit('password'),
        MustHaveUniqueChar('password'),
        MustHaveNonAlphanumeric('password'),
        DateMustBeValid('dayOfBirth', 'monthOfBirth')
      ]
  });
  }


get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.registerForm.invalid) {
      return;
    }

    this.statusCodeSvc.resetStatusCodes(this.sc);

    this.loading = true;
    this.authService.register(this.registerForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Registration successful, check your email and confirm your password', true);
          this.router.navigate(['/login']);
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.submitted = false;
          this.loading = false;
        });
  }
}
