import { Component, OnInit } from '@angular/core';
import { UserService } from '@core/_services/user.service';
import { FormBuilder, Form, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StatusCodes } from 'app/_models/StatusCodes';
import { StatusCodeService } from '@core/_services/Status-code.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  emailForm: FormGroup;

  sc: StatusCodes = new StatusCodes();

  loading: boolean;
  submitted: boolean;

  constructor(private userService: UserService,
              private fb: FormBuilder,
              private router: Router,
              private statusCodeSvc: StatusCodeService) { }

  ngOnInit() {
    this.emailForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get f() { return this.emailForm.controls; }

  findUserInfoId() {
    this.submitted = true;

    if (this.emailForm.invalid) {
      return;
    }

    this.statusCodeSvc.resetStatusCodes(this.sc);

    this.loading = true;

    this.userService.getUserInfoIdByEmail(this.emailForm.controls.email.value)
      .subscribe(
        data => {
          this.router.navigate([`/users/${data}/addrequest`]);
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.submitted = false;
          this.loading = false;
        });
  }
}
