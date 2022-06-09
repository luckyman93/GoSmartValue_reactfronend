import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {PassWordValidator} from "../../validators/password.validator";
import {AuthenticationService} from "../../services/authentication.service";
import {IRegister} from "../../../auth/models/interfaces/register";
import {Router} from "@angular/router";
import Swal from "sweetalert2";

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {

  profileEditForm: FormGroup = new FormGroup({});

  constructor(
    private router: Router,
    private fb: FormBuilder,
    private authService: AuthenticationService) { }

  ngOnInit(): void {
    this.initForm()
    this.authService.getUserDetails(this.authService.getToken()).subscribe(res => {
      this.populateForm(res.data);
    })
  }

  get f(): { [key: string]: AbstractControl } { return this.profileEditForm.controls; }

  private initForm(){
    this.profileEditForm = this.fb.group({
        firstName: ["", [Validators.required]],
        lastName: ["", [Validators.required]],
        phoneNumber: ["", [Validators.required]]
    })
  }

  private populateForm(person : any){
    this.profileEditForm = this.fb.group({
        firstName: [person.firstName, [Validators.required]],
        lastName: [person.lastName, [Validators.required]],
        phoneNumber: [person.phoneNumber, [Validators.required]]
    })
  }

  editProfileDetails(){
    let user = <any>{
      firstName: this.f.firstName.value,
      lastName: this.f.lastName.value,
      phoneNumber: this.f.phoneNumber.value,
    };
    this.authService.editProfileDetails(user).subscribe( res => {
      Swal.fire({
        position: 'top-end',
        title: 'Your profile has been updated!',
        icon: 'success',
        confirmButtonText: 'Ok'
      })
      this.router.navigate(["/profile"]);
    })
  }

}
