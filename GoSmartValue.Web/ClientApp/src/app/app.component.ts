import {Component, HostListener, Inject, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {AuthenticationService} from "./shared/services/authentication.service";
import {NavigationEnd, Router} from '@angular/router';

declare let gtag: Function;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations:[
    trigger('fade',
      [
        state('void', style({ opacity : 0})),
        transition(':enter',[ animate(300)]),
        transition(':leave',[ animate(500)]),
      ]
    )]
})
export class AppComponent {
  title = 'GoSmartValue';

  activeTabIndex: number = -1;
  sidebarClick: boolean = false;
  sidebarActive: boolean = false;
  layoutMode = 'static';
  documentClickListener: any;
  configActive: boolean = false;

  configClick: boolean = false;

  // @ts-ignore
  constructor(@Inject(DOCUMENT) document, public authService: AuthenticationService, public renderer: Renderer2,
               public router: Router) {
    this.router.events.subscribe(event => {
        if(event instanceof NavigationEnd){
          gtag('config', 'G-FL2YT2PMFV',
            {
              'page_path': event.urlAfterRedirects
            }
          );
        }
      }
    )
  }

  ngAfterViewInit(): void {
    this.documentClickListener = this.renderer.listen('body', 'click', (event) => {

      if (!this.sidebarClick && (this.overlay || !this.isDesktop())) {
        this.sidebarActive = false;
      }

      if (this.configActive && !this.configClick) {
        this.configActive = false;
      }

      this.configClick = false;
      this.sidebarClick = false;
    });
  }

  ngOnDestroy(): void {
    if (this.documentClickListener) {
      this.documentClickListener();
    }
  }

  onSidebarClick() {
    this.sidebarClick = true;
  }

  closeSidebar(event: Event) {
    this.sidebarActive = false;
    event.preventDefault();
  }

  get overlay(): boolean {
    return this.layoutMode === 'overlay';
  }

  isDesktop() {
    return window.innerWidth > 1024;
  }

  onTabClick(event: Event, index: number) {
    if (this.activeTabIndex === index) {
      this.sidebarActive = !this.sidebarActive;
    } else {
      this.activeTabIndex = index;
      this.sidebarActive = true;
    }

    event.preventDefault();
  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(e : any) {
    if (window.pageYOffset > 40) {
      let element = document.getElementById('navbar');
      // @ts-ignore
      element.classList.add('sticky');
    } else {
      let element = document.getElementById('navbar');
      // @ts-ignore
      element.classList.remove('sticky');
    }
  }

  isAuthenticated() {
    return this.authService.isAuthenticated();
  }
}
