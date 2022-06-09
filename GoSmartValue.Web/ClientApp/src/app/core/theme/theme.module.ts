import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {VendorModule} from "../../vendor/vendor.module";
import { SideNavComponent } from './navigation/side-nav/side-nav.component';
import { SideNavTabContentComponent } from './navigation/side-nav-tab-content/side-nav-tab-content.component';
import {TopNavComponent} from "./navigation/top-nav/top-nav.component";
import {FooterComponent} from "./footer/footer.component";
import {AuthModule} from "../../auth/auth.module";
import { MenuComponent } from './navigation/menu/menu.component';
import { MenuItemComponent } from './navigation/menu-item/menu-item.component';
import { TrendsMenuComponent } from './navigation/trends-menu/trends-menu.component';
import { ValuationsMenuComponent } from './navigation/valuations-menu/valuations-menu.component';

@NgModule({
  declarations: [
    TopNavComponent,
    FooterComponent,
    SideNavComponent,
    SideNavTabContentComponent,
    MenuComponent,
    MenuItemComponent,
    TrendsMenuComponent,
    ValuationsMenuComponent
  ],
  exports: [
    TopNavComponent,
    FooterComponent,
    SideNavComponent,
    SideNavTabContentComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    VendorModule,
    AuthModule
  ]
})
export class ThemeModule { }
