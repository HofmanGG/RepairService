import { Component, OnInit } from '@angular/core';
import { CountryService } from '@core/_services/country.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService } from '@core/_services/alert.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-add-country',
  templateUrl: './add-country.component.html',
  styleUrls: ['./add-country.component.css']
})
export class AddCountryComponent implements OnInit {
  addNewCountryForm: FormGroup;

  loading: boolean;
  submitted: boolean;

  sc: StatusCodes = new StatusCodes();

  constructor(
    private formBuilder: FormBuilder,
    private countryService: CountryService,
    private statusCodeSvc: StatusCodeService
  ) {
  }

  ngOnInit() {
    this.addNewCountryForm = this.formBuilder.group({
      countryName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(30)
      ]]
    });
  }

  get f() { return this.addNewCountryForm.controls; }

  AddNewCountry() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.addNewCountryForm.invalid) {
      return;
    }

    this.loading = true;

    this.countryService.addCountry(this.addNewCountryForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;
          this.addNewCountryForm.reset();

          this.submitted = false;
          this.loading = false;
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.submitted = false;
          this.loading = false;
        });
  }
}
