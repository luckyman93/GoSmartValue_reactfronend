import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { LoginSignupModalComponent } from './components/login-signup-modal/login-signup-modal.component';
import {VendorModule} from "../vendor/vendor.module";
import { UserTypeSelectorComponent } from './components/user-type-selector/user-type-selector.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';



@NgModule({
  declarations: [
    RegisterComponent,
    LoginComponent,
    LoginSignupModalComponent,
    UserTypeSelectorComponent,
    ResetPasswordComponent,
    ChangePasswordComponent
  ],
    exports: [
        LoginSignupModalComponent,
        RegisterComponent
    ],
    imports: [
        CommonModule,
        VendorModule,
    ]
})
export class AuthModule { }
