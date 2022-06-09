import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../shared/services/authentication.service";
import {Router} from "@angular/router";
import Swal from "sweetalert2";

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  email = "";
  changePasswordForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder,
              private authService: AuthenticationService,
              private router: Router) { }

  get f(): { [key: string]: AbstractControl } { return this.changePasswordForm.controls; }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(){
    this.changePasswordForm = this.fb.group({
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]]
    })
  }

  onChangePassword(){
    const token = this.authService.getToken();

    this.authService.getUserDetails(token).subscribe(res => {
      this.email = res.data.email;
    })

    var userName = {
      email: this.email,
      password: this.f.password.value,
      confirmPassword: this.f.confirmPassword.value,
      token: token,
    }
    this.authService.forgotPassword(userName).subscribe(res => {
      Swal.fire({
        position: 'top-end',
        title: 'Your email has been reset, check you email for reset link!',
        icon: 'success',
        confirmButtonText: 'Ok'
      })
    })
  }

}
