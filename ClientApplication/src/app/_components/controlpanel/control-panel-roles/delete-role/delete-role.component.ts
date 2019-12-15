import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { AlertService } from '@core/_services/alert.service';
import { RoleService } from '@core/_services/role.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-delete-role',
  templateUrl: './delete-role.component.html',
  styleUrls: ['./delete-role.component.css']
})
export class DeleteRoleComponent implements OnInit {
  deleteRoleForm: FormGroup;

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
    this.deleteRoleForm = this.fb.group({
      id: ['', Validators.required]
    });
  }
  get f() { return this.deleteRoleForm.controls; }

  DeleteRole() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.deleteRoleForm.invalid) {
      return;
    }

    this.loading = true;

    this.roleService.deleteRole(this.deleteRoleForm.controls.id.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;

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
