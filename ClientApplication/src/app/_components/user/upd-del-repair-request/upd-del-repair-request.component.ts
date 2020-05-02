import { Component, OnInit } from '@angular/core';
import { RepairRequest } from 'app/_models/RepairRequestModels/RepairRequest';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { UpdateRepairRequest } from 'app/_models/RepairRequestModels/UpdateRepairRequest';
import { RepairRequestService } from '@core/_services/repairRequest.service';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-upd-del-repair-request',
  templateUrl: './upd-del-repair-request.component.html',
  styleUrls: ['./upd-del-repair-request.component.css']
})
export class UpdDelRepairRequestComponent implements OnInit {

  repairRequests: RepairRequest[];
  repairStatuses: string[];

  repairRequestsForm: FormGroup;

  userInfoIdParam: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private repairRequestService: RepairRequestService) { }

  ngOnInit() {
    this.userInfoIdParam = this.route.parent.snapshot.params['id'];

    this.repairRequestsForm = new FormGroup({
      repairRequests: this.fb.array([])
    });

    this.repairRequestService.getRepairRequests(this.userInfoIdParam)
    .subscribe(rr => this.initFormArray(
      rr.sort((a, b) => b.requestYear - a.requestYear || b.requestMonth - a.requestMonth || b.requestDay - a.requestDay)
    ));

    this.repairStatuses = ['Received', 'In Progress', 'Done', 'Closed'];
  }

  get f() { return this.repairRequestsForm.get('repairRequests') as FormArray; }
   fg(index: number) { return ((this.repairRequestsForm.get('repairRequests') as FormArray).at(index) as FormGroup).controls; }

    createForms(repairRequest: RepairRequest): FormGroup {
      const formGroup: FormGroup = this.fb.group(
        {
          id: [repairRequest.id],

          productName: [repairRequest.productName, [Validators.required, Validators.minLength(4), Validators.maxLength(40)]],
          comment: [repairRequest.comment, [Validators.required, Validators.minLength(4), Validators.maxLength(100)]],

          requestDate: [`${repairRequest.requestDay}-${repairRequest.requestMonth}-${repairRequest.requestYear}`],

          repairStatus: [repairRequest.repairStatus],

          updateSuccess: [false],
          updateBadRequest: [false],
          updateNotFound: [false],
          updateUnknownError: [false],

          delSuccess: [false],
          delBadRequest: [false],
          delNotFound: [false],
          delUnknownError: [false],

          submitted: [false],

          updLoading: [false],
          delLoading: [false]
        }
      );
      return formGroup;
    }

    resetStatuses(index: number) {
      const localFg = this.fg(index);

      localFg.updateSuccess.setValue(false);
      localFg.updateBadRequest.setValue(false);
      localFg.updateNotFound.setValue(false);
      localFg.updateUnknownError.setValue(false);

      localFg.delSuccess.setValue(false);
      localFg.delBadRequest.setValue(false);
      localFg.delNotFound.setValue(false);
      localFg.delUnknownError.setValue(false);
    }

    initFormArray(repairRequests: RepairRequest[]) {
      const formArray = this.repairRequestsForm.get('repairRequests') as FormArray;
      repairRequests.map(item => {
        formArray.push(this.createForms(item));
      });

      this.repairRequestsForm.setControl('repairRequests', formArray);
    }

    updateRepairRequest(index: number) {
      const localFg = this.fg(index);

      localFg.submitted.setValue(true);

      this.resetStatuses(index);

      if (this.f.at(index).invalid) {
        return;
      }

      localFg.updLoading.setValue(true);

      const repairRequest: UpdateRepairRequest = this.f.at(index).value;
      this.repairRequestService
        .updateRepairRequest(this.userInfoIdParam, repairRequest.id, repairRequest)
      .pipe(first())
      .subscribe(
        data => {
          localFg.updateSuccess.setValue(true);
          localFg.updLoading.setValue(false);
        },
        error => {
          if (error.status === 400) {
            localFg.updateBadRequest.setValue(true);
          } else if (error.status === 404) {
            localFg.updateNotFound.setValue(true);
          } else {
            localFg.updateUnknownError.setValue(true);
          }
          localFg.updLoading.setValue(false);
        });
    }

    deleteRepairRequest(index: number) {
      const localFg = this.fg(index);

      this.resetStatuses(index);

      if (this.f.at(index).invalid) {
        return;
      }

      localFg.delLoading.setValue(true);

      const repairRequest = this.f.at(index).value;
      this.repairRequestService
        .deleteRepairRequest(this.userInfoIdParam, repairRequest.id)
        .pipe(first())
        .subscribe(
          data => { 
            localFg.delSuccess.setValue(true);
            localFg.delLoading.setValue(false);
          },
          error => {
            if (error.status === 400) {
              localFg.delBadRequest.setValue(true);
            } else if (error.status === 404) {
              localFg.delNotFound.setValue(true);
            } else {
                localFg.delUnknownError.setValue(true);
            }
            localFg.delLoading.setValue(false);
          });
    }
}

