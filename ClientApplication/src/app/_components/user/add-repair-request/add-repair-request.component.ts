import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RepairRequestService } from '@core/_services/repairRequest.service';
import { first } from 'rxjs/operators';
import { StatusCodeService } from '@core/_services/Status-code.service';
import { StatusCodes } from 'app/_models/StatusCodes';

@Component({
  selector: 'app-add-repair-request',
  templateUrl: './add-repair-request.component.html',
  styleUrls: ['./add-repair-request.component.css']
})
export class AddRepairRequestComponent implements OnInit {

  userInfoIdParam: number;
  repairRequestForm: FormGroup;

  sc: StatusCodes = new StatusCodes();

  submitted: boolean;
  loading: boolean;

  repairStatuses: string[] = ['Received', 'In Progress', 'Done', 'Closed'];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private repairRequestSvc: RepairRequestService,
    private statusCodeSvc: StatusCodeService
    ) { }

  ngOnInit() {
    this.userInfoIdParam = this.route.parent.snapshot.params['id'];

    this.repairRequestForm = this.fb.group({
          productName: ['', [Validators.required, /*Validators.minLength(4),*/ Validators.maxLength(40)]],
          comment: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],

          repairStatus: ['', [Validators.required]]
    });

  }

  get f() { return this.repairRequestForm.controls; }

  addRepairRequest() {
    this.submitted = true;

    this.statusCodeSvc.resetStatusCodes(this.sc);

    if (this.repairRequestForm.invalid) {
      return;
    }

    this.loading = true;

    this.repairRequestSvc.addRepairRequest(this.userInfoIdParam, this.repairRequestForm.value)
      .pipe(first())
        .subscribe(
        data => {
          this.sc.success = true;
          this.repairRequestForm.reset();

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
