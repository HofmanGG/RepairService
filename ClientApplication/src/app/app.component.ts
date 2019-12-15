import { Component } from '@angular/core';
import { Router } from '@angular/router';

import './_content/app.css';
import { AuthenticationService } from './core/_services/authentication.service';
import { User } from './_models/User';

@Component({ selector: 'app', 
templateUrl: 'app.component.html',
  styleUrls: ['./app.component.css']})
export class AppComponent {
    currentUser: User;

    constructor(
        private router: Router,
        private authSvc: AuthenticationService
    ) {
        this.authSvc.currentUser.subscribe(x => this.currentUser = x);
    }

    logout(): void {
        this.authSvc.logout();
        this.router.navigate(['/login']);
    }
}
