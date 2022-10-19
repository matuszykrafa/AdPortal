import { Component, OnInit } from '@angular/core';
import { UserType } from 'src/app/enums/user-type.enum';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  username: string | null
  constructor(private userService: UserService) { }

  get isLogged(): boolean {
    this.username = this.userService.currentUserName
    return this.userService.isUserAuthenticated;
  }
  ngOnInit(): void {
  }
  get userIsAdmin(): boolean {
    var role = this.userService.currentUserRole;
    if (role == UserType[UserType.ADMIN]) return true;
    return false;
  }
}
