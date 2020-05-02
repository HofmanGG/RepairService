import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/User';
import { AuthenticationService } from 'app/core/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService} from '@core/_services/alert.service';
import { CountryService } from '@core/_services/country.service';
import { DateMustBeValid } from 'app/_validators/date-must-be-valid.validator';
import { UserService } from '@core/_services/user.service';
import { StatusCodes } from 'app/_models/StatusCodes';
import { StatusCodeService } from '@core/_services/Status-code.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  changeInfoForm: FormGroup;

  loading: boolean;
  submitted: boolean;

  sc: StatusCodes = new StatusCodes();

  returnUrl: string;

  currentUser: User;

  genders: Array<string>;

  days: Array<number>;
  months: Array<number>;
  years: Array<number>;


  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private userService: UserService,
    private countryService: CountryService,
    private statusCodeSvc: StatusCodeService
  ) {
    this.currentUser = this.authenticationService.currentUserValue;

    this.genders = ['Male', 'Female'];

    this.days = Array(31).fill(0).map((x, i) => i + 1);
    this.months = Array(12).fill(0).map((x, i) => i + 1);
    this.years = Array(94).fill(0).map((x, i) => -i + 2013);
  }

  ngOnInit() {
    this.changeInfoForm = this.fb.group({
      id: [this.currentUser.id, [Validators.required]],
      appIdentityUserId: [this.currentUser.appIdentityUserId, [Validators.required]],
      firstName: [this.currentUser.firstName, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20)]
      ],
      lastName: [this.currentUser.lastName, [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(20)]
      ],
      gender: [this.currentUser.gender, [Validators.required]],
      dayOfBirth: [this.currentUser.dayOfBirth, [Validators.required]],
      monthOfBirth: [this.currentUser.monthOfBirth, [Validators.required]],
      yearOfBirth: [this.currentUser.yearOfBirth, [Validators.required]],
      countryId: [this.currentUser.countryId, [Validators.required]]
  }, {
    Validators: [
      DateMustBeValid('dayOfBirth', 'monthOfBirth')
    ]
  });

    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.changeInfoForm.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.changeInfoForm.invalid) {
      return;
    }

    this.loading = true;

    this.userService.updateUser(this.changeInfoForm.value.id, this.changeInfoForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.authenticationService.updateUser(data);

          this.alertService.success('Your Account Information is Successfully Changed!!!', true);
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.submitted = true;
          this.loading = false;
        });
  }
}
