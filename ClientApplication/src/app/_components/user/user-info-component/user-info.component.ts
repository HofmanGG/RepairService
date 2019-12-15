import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '@core/_services/user.service';
import { User } from 'app/_models/User';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.css']
})
export class UserInfoComponent implements OnInit {

  user$: Observable<User>;

  userInfoIdParam: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private userService: UserService) { }

  ngOnInit() {
    this.route.paramMap
      .subscribe(params => this.userInfoIdParam = +params.get('id'));

    this.user$ = this.userService.getUser(this.userInfoIdParam);
  }
}
