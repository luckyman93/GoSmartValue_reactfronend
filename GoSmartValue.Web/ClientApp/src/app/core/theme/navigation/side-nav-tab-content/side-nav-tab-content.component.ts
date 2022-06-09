import { Component, OnInit } from '@angular/core';

@Component({
  /* tslint:disable:component-selector */
  selector: 'app-side-nav-tab-content',
  /* tslint:enable:component-selector */
  template: `
        <div class="layout-submenu-content">
            <ng-content></ng-content>
        </div>
    `
})
export class SideNavTabContentComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
