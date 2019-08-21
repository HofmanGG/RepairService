import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/User';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { UserService } from 'src/app/_services/user.service';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from 'src/app/_services/alert.service';
import { CountryService } from 'src/app/_services/country.service';
import { Country } from 'src/app/_models/Country';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {
  changeInfoForm: FormGroup;
  loading = false;
  submitted = false; 
  returnUrl: string;
  
  currentUser: User;
  countries: Country[]

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private authenticationService: AuthenticationService,
      private alertService: AlertService,
      private countryService: CountryService
  ) {
      this.currentUser = this.authenticationService.currentUserValue;
  }

  ngOnInit() {
    this.changeInfoForm = this.formBuilder.group({
      firstName: [this.currentUser.firstName],
      lastName: [this.currentUser.lastName],
      gender: [this.currentUser.gender],
      dayOfBirth: [this.currentUser.dayOfBirth], 
      monthOfBirth: [this.currentUser.monthOfBirth], 
      yearOfBirth: [this.currentUser.yearOfBirth], 
      countryId: [this.currentUser.countryId]
  });
  
  this.countryService.getCountries()
  .subscribe((countryModels: Country[]) => this.countries = countryModels)

      // get return url from route parameters or default to '/'
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.changeInfoForm.controls; }

  onSubmit() {
      this.submitted = true;

      // reset alerts on submit
      this.alertService.clear();

      // stop here if form is invalid
      if (this.changeInfoForm.invalid) {
          return;
      }

      this.loading = true;
      let v = this.changeInfoForm.value;

      this.authenticationService.changeInfo(
        v.firstName,
        v.lastName,
        v.gender,
        v.dayOfBirth,
        v.monthOfBirth,
        v.yearOfBirth,
        v.countryId)
          .pipe(first())
          .subscribe(
              data => {
                  this.router.navigate([this.returnUrl]);
              },
              error => {
                  this.alertService.error(error);
                  this.loading = false;
              });
  }
}