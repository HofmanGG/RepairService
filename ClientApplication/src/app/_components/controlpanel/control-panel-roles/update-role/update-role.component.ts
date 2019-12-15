import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { RoleService } from '@core/_services/role.service';
import { StatusCodes } from 'app/_models/StatusCodes';
import { StatusCodeService } from '@core/_services/Status-code.service';

@Component({
  selector: 'app-update-role',
  templateUrl: './update-role.component.html',
  styleUrls: ['./update-role.component.css']
})
export class UpdateRoleComponent implements OnInit {

  updateRoleForm: FormGroup;

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
    this.updateRoleForm = this.fb.group({
      id: ['', Validators.required],
      name: ['', [
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(15)
      ]]
    });
  }

  get f() { return this.updateRoleForm.controls; }

  UpdateRole() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.updateRoleForm.invalid) {
      return;
    }

    this.loading = true;

    this.roleService.updateRole(this.updateRoleForm.value.id, this.updateRoleForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;
          this.updateRoleForm.controls['name'].reset();

          this.loading = false;
          this.submitted = false;
        },
        error => {
          this.statusCodeSvc.changeStatusCode(this.sc, error.status);

          this.submitted = true;
          this.loading = false;
        });
  }

}
