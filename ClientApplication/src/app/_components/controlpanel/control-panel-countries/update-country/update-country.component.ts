import { Component, OnInit } from '@angular/core';
import { CountryService } from '@core/_services/country.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AlertService } from '@core/_services/alert.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-update-country',
  templateUrl: './update-country.component.html',
  styleUrls: ['./update-country.component.css']
})
export class UpdateCountryComponent implements OnInit {
  updateCountryForm: FormGroup;

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
    this.updateCountryForm = this.formBuilder.group({
      countryId: ['', Validators.required],
      countryName: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(30)]
      ]
    });
  }

  get f() { return this.updateCountryForm.controls; }

  UpdateCountry() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.updateCountryForm.invalid) {
      return;
    }

    this.loading = true;

    this.countryService.updateCountry (this.updateCountryForm.value.countryId, this.updateCountryForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;
          this.updateCountryForm.controls['countryName'].reset();

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
