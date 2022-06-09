import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from "../../../../services/authentication.service";

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss']
})
export class UserDetailsComponent implements OnInit {

  user: any;

  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.getUserDetails();
  }

  getUserDetails(){
    this.authService.getUserDetails(this.authService.getToken()).subscribe(res => {
      this.user= res.data;
    })
  }

}
