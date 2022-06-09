import {AfterViewInit, Component, OnInit, Renderer2} from '@angular/core';
import {AppComponent} from "../../../../app.component";
import {MatDialog} from '@angular/material/dialog'
import {AuthenticationService} from "../../../../shared/services/authentication.service";
import {User} from "../../../../auth/models/user";
import {Router} from "@angular/router";
import {DialogService} from 'primeng/dynamicdialog';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {LoginSignupModalComponent} from "../../../../auth/components/login-signup-modal/login-signup-modal.component";

@Component({
  selector: 'app-top-nav',
  templateUrl: './top-nav.component.html',
  styleUrls: ['./top-nav.component.scss'],
  providers: [DialogService, LoginSignupModalComponent]
})
export class TopNavComponent implements OnInit, AfterViewInit {

  topbarMenuActive: boolean = false;

  topbarItemClick: boolean = false;

  activeTopbarItem: any;

  documentClickListener: any;

  displayModal: boolean = false;

  loginOrRegister: boolean = false;

  username = "";

  loggedInUser : string = "";

  ref: DynamicDialogRef = new DynamicDialogRef();
  userPortal: any;

  constructor(public appMain: AppComponent,
              public dialog : MatDialog,
              private authService: AuthenticationService,
              public renderer: Renderer2,
              private router: Router,
              public dialogService: DialogService,
              private loginSignupModalComponent: LoginSignupModalComponent) { }

  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event.constructor.name === "NavigationEnd") {
        if(this.authService.isAuthenticated()){
          this.getUser();
        }
      }
    })
    this.getUserPortal();
  }

  ngAfterViewInit() {
    this.documentClickListener = this.renderer.listen('body', 'click', (event) => {
      if (!this.topbarItemClick) {
        this.activeTopbarItem = null;
        this.topbarMenuActive = false;
      }
    });
  }

  onTopbarMenuButtonClick(event : Event) {
    this.topbarItemClick = true;
    this.topbarMenuActive = !this.topbarMenuActive;

    event.preventDefault();
  }

  onTopbarItemClick(event: { preventDefault: () => void; }, item: any) {
    this.topbarItemClick = true;

    if (this.activeTopbarItem === item) {
      this.activeTopbarItem = null;
    }
    else {
      this.activeTopbarItem = item;
    }

    event.preventDefault();
  }

  openLoginSignUpDialog(loginOrRegister: boolean) {

    this.loginOrRegister = loginOrRegister;

    this.ref = this.dialogService.open(LoginSignupModalComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        loginOrRegister: loginOrRegister
      }
    });

    this.ref.onClose.subscribe(res => {
      this.closeLoginSignUpDialog();
    })
  }

  closeLoginSignUpDialog(){
    this.ref.destroy()
  }

  isAuthenticated() {
    return this.authService.isAuthenticated();
  }

  logout() {
    return this.authService.logOut();
  }

  getUser(){
    this.loggedInUser = this.authService.getFullname();
  }

  getUserPortal(){
      this.authService.getCurrentUser().subscribe(res => {
        this.userPortal = res.data.portalUrl;
        // this.goHome();
      });

  }
  goHome() {
    switch(this.userPortal) {
      case 'corporate':
        this.router.navigateByUrl('/corporate');
      break;

      case 'valuer':
        this.router.navigateByUrl('/valuer');
      break;

      case '/portal':
        this.router.navigateByUrl('/portal');
      break;

        default:
          this.router.navigateByUrl('/');
        break;
    }
  }

    onSubscribe() {
      this.activeTopbarItem = null;
      switch(this.userPortal) {
        case 'corporate':
          this.router.navigateByUrl('/corporate/subscriptions');
        break;

        case 'valuer':
          this.router.navigateByUrl('/valuer/subscriptions');
        break;

        case '/portal':
          this.router.navigateByUrl('/portal/subscriptions');
        break;

          default:
            this.router.navigateByUrl('/');
          break;
      }
    }
}
