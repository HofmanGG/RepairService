import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { Role } from 'app/_models/RoleModels/Role';
import { NewRole } from 'app/_models/RoleModels/NewRole';
import { UpdateRole } from 'app/_models/RoleModels/UpdateRole';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private userUrl = 'https://localhost:44305/api/roles';

  roles = new BehaviorSubject<Role[]>(null);

    constructor(private http: HttpClient) {
      this.getRoles().subscribe(
        returnedRoles => this.roles.next(returnedRoles)
    );
  }

  public getRoles() {
    const url = this.userUrl;
    return this.http.get<Role[]>(this.userUrl)
      .pipe(map((roles: Role[]) => {
        return roles.sort((a, b) => a.name.localeCompare(b.name));
      }));
  }

  public addRole(role: NewRole) {
    const url = this.userUrl;
    return this.http.post(url, role);
  }

  public updateRole(roleId: string, role: UpdateRole) {
    const url = `${this.userUrl}/${roleId}`;
    return this.http.put(url, role);
  }

  public deleteRole(roleId: string) {
    const url = `${this.userUrl}/${roleId}`;
    return this.http.delete(url);
  }
}
