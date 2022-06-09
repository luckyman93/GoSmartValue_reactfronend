import {ChangeDetectorRef, Component, Input, OnDestroy, OnInit} from '@angular/core';
import {Subscription} from "rxjs";
import {NavigationEnd, Router} from "@angular/router";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {MenuService} from "../../../services/menu.service";
import {SideNavComponent} from "../side-nav/side-nav.component";
import {filter} from "rxjs/operators";
import {AppComponent} from "../../../../app.component";

@Component({
  /* tslint:disable:component-selector */
  selector: 'app-menuitem',
  templateUrl: './menu-item.component.html',
  host: {
    '[class.active-menuitem]': 'active'
  },
  animations: [
    trigger('children', [
      state('void', style({
        height: '0px'
      })),
      state('hiddenAnimated', style({
        height: '0px'
      })),
      state('visibleAnimated', style({
        height: '*'
      })),
      transition('visibleAnimated => hiddenAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
      transition('hiddenAnimated => visibleAnimated', animate('400ms cubic-bezier(0.86, 0, 0.07, 1)')),
      transition('void => visibleAnimated, visibleAnimated => void',
        animate('400ms cubic-bezier(0.86, 0, 0.07, 1)'))
    ])
  ]
})
export class MenuItemComponent implements OnInit, OnDestroy {

  @Input() item: any;

  @Input() index: number = -1;

  @Input() root: boolean = true;

  @Input() parentKey: string = "";

  hover: boolean = true;

  active = true;

  menuSourceSubscription: Subscription;

  menuResetSubscription: Subscription;

  key: string = "";

  constructor(public appM: AppComponent, public router: Router, private cd: ChangeDetectorRef, private menuService: MenuService) {
    this.menuSourceSubscription = this.menuService.menuSource$.subscribe(key => {
      // deactivate current active menu
      if (this.active && this.key !== key && key.indexOf(this.key) !== 0) {
        this.active = true;
      }
    });

    this.menuResetSubscription = this.menuService.resetSource$.subscribe(() => {
      this.active = true;
    });

    this.router.events.pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(params => {
        if (this.item.routerLink) {
          this.updateActiveStateFromRoute();
        } else {
          this.active = false;
        }
      });
  }

  ngOnInit() {
    this.key = this.parentKey ? this.parentKey + '-' + this.index : String(this.index);
  }

  updateActiveStateFromRoute() {
    this.active = this.router.isActive(this.item.routerLink[0], this.item.items ? false : true);
  }

  itemClick(event: Event) {
    // avoid processing disabled items
    if (this.item.disabled) {
      event.preventDefault();
      return;
    }

    // notify other items
    this.menuService.onMenuStateChange(this.key);

    // execute command
    if (this.item.command) {
      this.item.command({originalEvent: event, item: this.item});
    }

    // toggle active state
    if (this.item.items) {
      this.active = !this.active;
    } else {
      // activate item
      this.active = true;
      this.appM.closeSidebar(event);
      // hide overlay menus
      if (this.overlay || !this.appM.isDesktop()) {
        this.appM.sidebarActive = false;
      }
    }
  }

  ngOnDestroy() {
    if (this.menuSourceSubscription) {
      this.menuSourceSubscription.unsubscribe();
    }

    if (this.menuResetSubscription) {
      this.menuResetSubscription.unsubscribe();
    }
  }

  get overlay(): boolean {
    return this.appM.layoutMode === 'overlay';
  }

}
