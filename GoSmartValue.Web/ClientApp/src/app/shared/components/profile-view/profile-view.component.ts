import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PassWordValidator} from "../../validators/password.validator";
import {AuthenticationService} from "../../services/authentication.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile-view',
  templateUrl: './profile-view.component.html',
  styleUrls: ['./profile-view.component.scss']
})
export class ProfileViewComponent implements OnInit {

  person: any;

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.authService.getUserDetails(this.authService.getToken()).subscribe(res => {
      this.person = res.data;
    })
  }

  goToEdit() {
    this.router.navigate(["/profile/edit"]);
  }
}
