import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AlertService } from '@core/_services/alert.service';
import { RoleService } from '@core/_services/role.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent implements OnInit {
  addNewRoleForm: FormGroup;

  loading: boolean;
  submitted: boolean;

  sc: StatusCodes = new StatusCodes();

  constructor(
    private fb: FormBuilder,
    private roleService: RoleService,
    private statusCodeSvc: StatusCodeService
  ) {
  }

  ngOnInit() {
    this.addNewRoleForm = this.fb.group({
      name: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(15)
      ]]
    });
  }

  get f() { return this.addNewRoleForm.controls; }

  AddNewRole() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.addNewRoleForm.invalid) {
      return;
    }

    this.loading = true;

    this.roleService.addRole(this.addNewRoleForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;
          this.addNewRoleForm.reset();

          this.loading = false;
          this.submitted = false;
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.loading = false;
          this.submitted = false;
        });
  }
}
