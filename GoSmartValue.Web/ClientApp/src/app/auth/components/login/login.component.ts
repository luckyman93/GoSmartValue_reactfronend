import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Login} from "../../models/login";
import {AuthenticationService} from "../../../shared/services/authentication.service";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import Swal from "sweetalert2";
import {Router} from "@angular/router";
import {HttpErrorResponse} from "@angular/common/http";
import {LoginSignupModalComponent} from "../login-signup-modal/login-signup-modal.component";
import {DialogService} from 'primeng/dynamicdialog';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {ResetPasswordComponent} from "../reset-password/reset-password.component";
import {IBasketProduct} from "../../../shared/models/interfaces/basket-product";
import {BasketProduct} from "../../../shared/models/basket-product";
import {BasketService} from "../../../shared/services/basket.service";
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @Output() closeLoginLogoutDialog = new EventEmitter<boolean>();
  @Input() basketItem : IBasketProduct = new BasketProduct();
  @Input() product : any;
  loginForm: FormGroup = new FormGroup({});
  loginData: Login = new Login();
  submitted: boolean = false;
  ref: DynamicDialogRef = new DynamicDialogRef();

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private router: Router,
    public dialogService: DialogService,
    private basketService: BasketService,
    private messageService: MessageService) { }

  get f(): { [key: string]: AbstractControl } { return this.loginForm.controls; }

  ngOnInit(): void {
    this.initForm();
  }

  private initForm(){
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', Validators.required],
    })
  }

  onLogin(){
    this.submitted = true;

    this.loginData = <Login>{
      userName: this.f.userName.value,
      password: this.f.password.value
    };

    this.login();
  }

  login(){
    this.authService.login(this.loginData).subscribe(res=>{

      this.authService.saveToken(res.token);
      this.authService.getUserDetails(res.token).subscribe( user => {
        this.closeLoginLogoutDialog.emit(true);
        this.authService.saveUserDetails(user.data);
        this.router.navigate([user.data.portalUrl]);

        if(this.basketItem != undefined){
          this.basketService.addToBasket(this.basketItem).subscribe(res => {
            Swal.fire({
              position: 'top-end',
              title: 'Successfully added ' + this.product.name + ' to your Basket!',
              icon: 'success',
              confirmButtonText: 'Ok'
            })
          })
        }
      });

    },(err: HttpErrorResponse) => {
      this.messageService.add({key: 'loginToast', severity:'error', summary: 'Login Failed', detail: err.error});
      this.submitted = false;
      return;
    })
  }

  openResetPassword(){

    this.ref = this.dialogService.open(ResetPasswordComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
    });
  }
}
