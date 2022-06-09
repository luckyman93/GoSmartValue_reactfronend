import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from "./core/core.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { LayoutModule } from '@angular/cdk/layout';
import { HomeModule } from "./home/home.module";
import {AuthModule} from "./auth/auth.module";

import {MessageService} from "primeng/api";
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { PortalCorporateModule } from './portal-corporate/portal-corporate.module';
import { PortalValuerModule } from './portal-valuer/portal-valuer.module';
import { PortalStandardUserModule } from './portal-standard-user/portal-standard-user.module';

@NgModule({
  declarations: [
    AppComponent
  ],
    imports: [
      AppRoutingModule,
      AuthModule,
      BrowserModule,
      BrowserAnimationsModule,
      CoreModule.forRoot(),
      LayoutModule,
      HomeModule,
      HttpClientModule,
      PortalCorporateModule,
      PortalValuerModule,
      PortalStandardUserModule

    ],
  providers: [MessageService, DialogService, DynamicDialogConfig, DynamicDialogRef],
  bootstrap: [AppComponent]
})
export class AppModule { }
