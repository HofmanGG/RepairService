import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RepairRequest } from 'app/_models/RepairRequestModels/RepairRequest';
import { UpdateRepairRequest } from 'app/_models/RepairRequestModels/UpdateRepairRequest';
import { NewRepairRequest } from 'app/_models/RepairRequestModels/NewRepairRequest';

@Injectable({
  providedIn: 'root'
})
export class RepairRequestService  {

    private rrUrl = `https://localhost:44305/api/users`;

    constructor(private http: HttpClient) {
    }

    public addRepairRequest(userInfoId: number, repairRequest: NewRepairRequest) {
      const url = `${this.rrUrl}/${userInfoId}/repairrequests/`;
      return this.http.post(url, repairRequest);
    }

    public updateRepairRequest(userInfoId: number, repairRequestId: number, repairRequest: UpdateRepairRequest) {
      const url = `${this.rrUrl}/${userInfoId}/repairrequests/${repairRequestId}`;
      return this.http.put(url, repairRequest);
    }

    public getRepairRequests(userInfoId: number): Observable<RepairRequest[]> {
      return this.http.get<RepairRequest[]>(`${this.rrUrl}/${userInfoId}/repairrequests`);
    }

    public deleteRepairRequest(userInfoId: number, repairRequestId: number) {
      const url = `${this.rrUrl}/${userInfoId}/repairrequests/${repairRequestId}`;
      return this.http.delete(url);
    }
}
