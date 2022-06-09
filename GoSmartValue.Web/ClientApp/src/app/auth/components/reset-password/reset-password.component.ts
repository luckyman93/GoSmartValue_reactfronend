import { Component, OnInit } from '@angular/core';
import {Login} from "../../models/login";
import {AuthenticationService} from "../../../shared/services/authentication.service";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import Swal from "sweetalert2";

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetForm: FormGroup = new FormGroup({});

  constructor(private fb: FormBuilder,
              private authService: AuthenticationService,
              private router: Router) { }

  get f(): { [key: string]: AbstractControl } { return this.resetForm.controls; }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(){
    this.resetForm = this.fb.group({
      userName: ['', [Validators.required]],
    })
  }

  onReset(){
    var userName = {
      userName : this.f.userName.value
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
