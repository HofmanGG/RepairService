import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/User';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  
})
export class UserComponent implements OnInit {

  userId: number;
  user: User;

  friendsCount: number;
  subsCount: number;

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
     this.userId = +this.route.snapshot.paramMap.get('userId');

     this.userService.getUser(this.userId)
     .subscribe((userModel: User) => this.user = userModel);
     
     this.userService.getCountOfFriends(this.userId)
     .subscribe((count: number) => this.friendsCount = count);

     this.userService.getCountOfSubs(this.userId)
     .subscribe((count: number) => this.subsCount = count);
  }

  deleteUser()
  {
    this.userService.deleteUser(this.userId);
  }
}