import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from 'app/_models/User';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private accountUrl = 'https://localhost:44305/api/accounts';

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private http: HttpClient) {
      this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
      this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
      return this.currentUserSubject.value;
    }

    public register(RegisterForm: FormData) {
      const url = `${this.accountUrl}/signup`;
      return this.http.post(url, RegisterForm);
    }

    public userIsManager(): boolean {
      return this.currentUserValue.roles.includes('Manager');
    }

    public userIsAdmin(): boolean {
      return this.currentUserValue.roles.includes('Admin');
    }

    public login(LoginForm: FormData): Observable<User> {
      const url = `${this.accountUrl}/signin`;
      return this.http.post<User>(url, LoginForm)
        .pipe(map(user => {
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          return user;
        }));
    }

    //не используется, заменен на user.service.updateaccount
    /*
    public updateUser(ChangedUserInfo: FormData): Observable<User> {
        const url = `${this.accountUrl}/changeinfo`;
        return this.http.post<User>(url, ChangedUserInfo)
        .pipe(map(user => {
          user.token = this.currentUserValue.token;
          user.roles = this.currentUserValue.roles;
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          return user;
      }));
    }
    */

    public updateUser(changedUserInfo: User): void {
      changedUserInfo.token = this.currentUserValue.token;
      changedUserInfo.roles = this.currentUserValue.roles;
      // store user details and jwt token in local storage to keep user logged in between page refreshes
      localStorage.setItem('currentUser', JSON.stringify(changedUserInfo));
      this.currentUserSubject.next(changedUserInfo);
    }

    public logout(): void {
        // remove user from local storage and set current user to null
      localStorage.removeItem('currentUser');
      this.currentUserSubject.next(null);
    }
}
