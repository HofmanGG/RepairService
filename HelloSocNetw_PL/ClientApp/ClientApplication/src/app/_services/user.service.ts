import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from 'src/app/_models/User';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private userUrl = `https://localhost:44305/api/users/`;
  private accountUrl = `https://localhost:44305/api/accounts/`

  register(
    email: string,
    password: string, 
    confirmPassword: string, 
    firstName: string, 
    lastName: string,
    gender: string,
    dayOfBirth: number,
    monthOfBirth: number,
    yearOfBirth: number,
    countryId: number){
    let body = JSON.stringify({ email, password, confirmPassword, firstName, lastName, gender, dayOfBirth, monthOfBirth, yearOfBirth, countryId});
    let headers={
      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      })
  }

    return this.http.post(this.accountUrl + "signup", body, headers);
  }  

  addAvatar(formData: FormData)
  {
    let url = this.userUrl + `avatar`;
    this.http.post(url, formData);
  }

  getUser(userId: number)
  {
    let url = this.userUrl + userId;
    return this.http.get(url);
  }

    getFriends(userId: number, toTake: number)
    {
    let url = this.userUrl + userId + `/friends`;
    return this.http.post(url, {toTake: toTake})
  }

  getSubs(userId: number, toTake: number)
  {
    let url = this.userUrl + + userId + `/subscribers`;
    return this.http.post(url, {toTake: toTake})
  }

  getUsers(takeFrom: number)
  {
    return this.http.post(this.userUrl, {from: takeFrom});
  }

  deleteUser(userId: number)
  {
    let url = this.userUrl + `delete/` + userId;
    return this.http.delete(url);
  }

  getCountOfUsers()
  {
    let url = this.userUrl + `count`;
    return this.http.get(url);
  }

  getCountOfFriends(userId: number)
  {
    let url = this.userUrl + userId + `/friends/count`;
    return this.http.get(url);
  }

  getCountOfSubs(userId: number)
  {
    let url = this.userUrl + userId + `/subscribers/count`;
    return this.http.get(url);
  }

  getProfilePicture(userId: number)
  {
    let url = this.userUrl + userId + `/profilepicture`
    return this.http.get(url);
  }
  
}
