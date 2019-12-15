import { Component, OnInit } from '@angular/core';
import { User } from 'app/_models/User';
import { RepairRequest } from 'app/_models/RepairRequestModels/RepairRequest';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute,  Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  userInfoIdParam: number;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit() {
    this.route.paramMap
      .subscribe(params => this.userInfoIdParam = +params.get('id'));
  }
}

