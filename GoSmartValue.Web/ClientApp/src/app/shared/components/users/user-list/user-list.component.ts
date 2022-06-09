import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../../../services/authentication.service";

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  users!: any[];
  constructor(private authenticationService : AuthenticationService) { }

  ngOnInit(): void {
    this.getAllUsers()
  }

  getAllUsers()
  {
    this.authenticationService.getAllUsers().subscribe((data:any)=> {
      this.users = data.data;
    });
  }

}
