import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/_services/user.service';
import { User } from 'src/app/_models/User';
import { AuthenticationService } from 'src/app/_services/authentication.service';


@Component({ 
  templateUrl: 'home.component.html',
  styleUrls: ['home.component.css'] })
export class HomeComponent implements OnInit {
    currentUser: User;

    constructor(
        private authenticationService: AuthenticationService,
        private userService: UserService
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
    }

    ngOnInit() {
    }
}