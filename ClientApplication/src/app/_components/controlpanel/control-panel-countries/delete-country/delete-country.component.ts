import { Component, OnInit } from '@angular/core';
import { CountryService } from '@core/_services/country.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService } from '@core/_services/alert.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-delete-country',
  templateUrl: './delete-country.component.html',
  styleUrls: ['./delete-country.component.css']
})
export class DeleteCountryComponent implements OnInit {
  deleteCountryForm: FormGroup;

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
    this.deleteCountryForm = this.formBuilder.group({
      countryId: ['', Validators.required]
    });
  }


  get f() { return this.deleteCountryForm.controls; }

  DeleteCountry() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.deleteCountryForm.invalid) {
      return;
    }

    this.loading = true;

    this.countryService.deleteCountry(this.deleteCountryForm.value.countryId)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;

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
