import { Component } from '@angular/core';
import { Router } from '@angular/router';

import './_content/app.css';
import { AuthenticationService } from './_services/authentication.service';
import { User } from './_models/User';

@Component({ selector: 'app', templateUrl: 'app.component.html' })
export class AppComponent {
    currentUser: User;

    constructor(
        private router: Router,
        private authenticationService: AuthenticationService
    ) {
        this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
    }

    logout() {
        this.authenticationService.logout();
        this.router.navigate(['/login']);
    }
}