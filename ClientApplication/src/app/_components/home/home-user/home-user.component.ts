import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '@core/_services/authentication.service';
import { User } from 'app/_models/User';

@Component({
  selector: 'app-home-user',
  templateUrl: './home-user.component.html',
  styleUrls: ['./home-user.component.css']
})
export class HomeUserComponent implements OnInit {

  currentUser: User;

    constructor(
        private authenticationService: AuthenticationService,
    ) {
        this.currentUser = this.authenticationService.currentUserValue;
    }

    ngOnInit() {
    }

}
