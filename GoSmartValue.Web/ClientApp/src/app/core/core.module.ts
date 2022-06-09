import {ModuleWithProviders, NgModule, Optional, SkipSelf} from '@angular/core';
import { CommonModule } from '@angular/common';
import {EnsureModuleLoadedOnceGuard} from "../ensure-module-loaded-once.guard";
import {RouterModule} from "@angular/router";
import {VendorModule} from "../vendor/vendor.module";

import {DialogModule} from 'primeng/dialog';
import {AuthModule} from "../auth/auth.module";
import {ThemeModule} from "./theme/theme.module";

@NgModule({
  declarations: [],
  imports: [
    ThemeModule,
    CommonModule,
    VendorModule,
    RouterModule,
    DialogModule,
    AuthModule
  ],
  exports: [
    ThemeModule,
    CommonModule,
    VendorModule,
    DialogModule,
    RouterModule
  ]
})
export class CoreModule extends EnsureModuleLoadedOnceGuard{
    constructor(@Optional() @SkipSelf() parentModule : CoreModule) {
      super(parentModule);
    }

    static forRoot(): ModuleWithProviders<CoreModule>{
        return {
          ngModule: CoreModule,
          providers: [

          ]
        }
    }
}
