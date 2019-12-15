import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { User } from 'app/_models/User';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private userUrl = 'https://localhost:44305/api/users';

  public getUser(userId: number): Observable<User>  {
    const url = `${this.userUrl}/${userId}`;
    return this.http.get<User>(url);
  }

  public getUsers(takeFrom: number): Observable<User[]> {
    const url = `${this.userUrl}`;
    return this.http.post<User[]>(url, {from: takeFrom});
  }

  public updateUser(userId: number, userInfo: User): Observable<User> {
    const url = `${this.userUrl}/${userId}`;
    return this.http.put<User>(url, userInfo);
  }

  public getCountOfUsers(): Observable<number> {
    const url = `${this.userUrl}/count`;
    return this.http.get<number>(url);
  }

  public getUserInfoIdByEmail(email: string) {
    const url = `${this.userUrl}/email/`;
    const params = new HttpParams().set('email', email);
    return this.http.get<any>(url, { params });
  }
}
