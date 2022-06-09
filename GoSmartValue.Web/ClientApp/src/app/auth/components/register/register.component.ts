import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {AuthenticationService} from "../../../shared/services/authentication.service";
import {IRegister} from "../../models/interfaces/register";
import {MessageService} from "primeng/api";
import Swal from "sweetalert2";
import {DialogService} from 'primeng/dynamicdialog';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {PassWordValidator} from "../../../shared/validators/password.validator";
import {TermsAndConditionsComponent} from "../../../shared/components/terms-and-conditions/terms-and-conditions.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registerForm: FormGroup = new FormGroup({});
  public barLabel = 'Password strength:';
  submitted: boolean = false;
  userExist: boolean = false;
  userType: any = 'standard';
  ref: DynamicDialogRef = new DynamicDialogRef();
  @Output() closeDialog = new EventEmitter<boolean>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private messageService: MessageService,
    public dialogService: DialogService
  ) { }

  get f(): { [key: string]: AbstractControl } { return this.registerForm.controls; }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(){
    this.registerForm = this.fb.group({
      userType:[this.userType,[Validators.required]],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      reacNumber:[''],
      reibNumber:[''],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{6,16}$/)]],
      confirmPassword: ['', [Validators.required]],
      termsAndConditions: ['', [Validators.required]],
      professionalConfirmation:[''],
      professionalType:['']
    },
    {
      validator: [PassWordValidator.passwordMatchValidator]
    })

    this.formControlValueChanged();
  }

  formControlValueChanged()
  {
    const reacControl = this.registerForm.get('reacNumber');
    const reibControl = this.registerForm.get('reibNumber');
    const profControl = this.registerForm.get('professionalConfirmation');
    const profTypeControl = this.registerForm.get('professionalType');
    this.registerForm.get('userType')?.valueChanges.subscribe(
      (type: string) => {
          if (type === 'valuer') {
            reacControl?.setValidators([Validators.required]);
            reibControl?.setValidators([Validators.required]);
            profControl?.setValidators([Validators.required]);
            profTypeControl?.setValidators([Validators.required]);
          }
          else if (type === 'standard' || type === 'corporate' ) {
            reacControl?.clearValidators();
            reibControl?.clearValidators();
            profControl?.clearValidators();
            profTypeControl?.clearValidators();
          }
          reacControl?.updateValueAndValidity();
      });
  }

  onRegister(){
    this.submitted = true;

    if(this.f.termsAndConditions.value[0] == 'accept') {

      if(this.f.userType.value == 'standard')
      {
        let user = <IRegister>{
          firstName: this.f.firstName.value,
          lastName: this.f.lastName.value,
          email: this.f.email.value,
          phoneNumber: this.f.phoneNumber.value,
          password: this.f.password.value
        };
        this.registerUser(user);
      }

      if(this.f.userType.value == 'valuer')
      {
        let valuerUser = <IRegister>{
          firstName: this.f.firstName.value,
          lastName: this.f.lastName.value,
          email: this.f.email.value,
          phoneNumber: this.f.phoneNumber.value,
          password: this.f.password.value,
          reacNumber:this.f.reacNumber.value,
          reibNumber: this.f.reibNumber.value
        };
        this.registerUser(valuerUser);
      }
    }else{
      this.f.termsAndConditions.value == 'unread'
    }
  }

  registerUser(user : IRegister){
    if(this.userType == 'valuer'){
      this.authService.valuerRegistration(user).subscribe((res)=>{
        if(res.statusCode == 200){
          this.closeDialog.emit(false);
          Swal.fire({
            position: 'top-end',
            title: 'Successfully Created Account!',
            text: 'Go to you email and very the account',
            icon: 'success',
            confirmButtonText: 'Ok'
          })
        }
      })
    }else{
      this.authService.standardUserRegistration(user).subscribe((res)=>{
        if(res.statusCode == 200){
          this.closeDialog.emit(false);
          Swal.fire({
            position: 'top-end',
            title: 'Successfully Created Account!',
            text: 'Go to you email and very the account',
            icon: 'success',
            confirmButtonText: 'Ok'
          })
        }
      })
    }

  }

  isRegistered() {
    this.authService.isRegistered(this.f.email.value).subscribe(res => {
      this.userExist = res.data.isRegistered ?this.userExist=true :this.userExist=false;
      return this.userExist;
    })
  }

  login() {

  }

  openTerms() {
    this.ref = this.dialogService.open(TermsAndConditionsComponent, {
      header: 'Terms and Conditions',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10001,
    });
  }

  selectUser(event: any)
  {
    this.userType = event;
  }
}
