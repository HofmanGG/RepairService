import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'src/app/_models/User';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private authUrl = `https://localhost:44305/api/accounts/`
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(email: string, password: string) {
        let url = this.authUrl + `signin`;
        let body = { email, password };
        let headers={
            headers: new HttpHeaders({
            'Content-Type': 'application/json'
        })};
        return this.http.post<any>(url, body, headers)
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    changeInfo(
        firstName: string, 
        lastName: string,
        gender: string,
        dayOfBirth: number,
        monthOfBirth: number,
        yearOfBirth: number,
        countryId: number){
        let url = this.authUrl + `changeinfo`
        let body = JSON.stringify({firstName, lastName, gender, dayOfBirth, monthOfBirth, yearOfBirth, countryId});
        let headers={
          headers: new HttpHeaders({
              'Content-Type': 'application/json'
          })
      }
    
      return this.http.post<any>(url, body, headers)
      .pipe(map(user => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          return user;
      }));
      }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }
}