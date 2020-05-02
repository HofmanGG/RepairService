import { Component, OnInit } from '@angular/core';
import { RepairRequestService } from '@core/_services/repairRequest.service';
import { RepairRequest } from 'app/_models/RepairRequestModels/RepairRequest';
import { AuthenticationService } from '@core/_services/authentication.service';

@Component({
  selector: 'app-home-requests',
  templateUrl: './home-requests.component.html',
  styleUrls: ['./home-requests.component.css']
})
export class HomeRequestsComponent implements OnInit {
  repairRequests: RepairRequest[];

  userInfoId: number;

  constructor(
    private repairRequestService: RepairRequestService,
    private authSvc: AuthenticationService) { }

  ngOnInit() {
    this.userInfoId = this.authSvc.currentUserValue.id;

    this.repairRequestService.getRepairRequests(this.userInfoId)
    .subscribe(rr =>
      this.repairRequests = rr.sort((a, b) => b.requestYear - a.requestYear ||
      b.requestMonth - a.requestMonth ||
      b.requestDay - a.requestDay)
    );
  }
}
