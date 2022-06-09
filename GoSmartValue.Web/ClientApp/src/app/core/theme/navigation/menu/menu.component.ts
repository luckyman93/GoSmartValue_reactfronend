import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/shared/services/authentication.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit {

  public model: any[] = [];
  private user!: any;

  constructor(private authService: AuthenticationService){

  }

  ngOnInit() {
    this.getCurrentUser();
  }

  getCurrentUser()
  {
    this.authService.getCurrentUser().subscribe(res => {
      this.user = res
      this.setModel();
    });
  }
  setModel() {
    switch(this.user.data.portalUrl) {
      case 'corporate':
        this.model = [
          {label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/']},
          {
            label: 'Valuations', icon: 'pi pi-fw pi-star-o', badge: 6,
            items: [
              {label: 'Issue Instruction', icon: 'pi pi-fw pi-directions', routerLink: ['/valuation/issue-instruction']},
              {label: 'Instruction History', icon: 'pi pi-fw pi-list', routerLink: ['/corporate/instructions']},
              {label: 'Instant Valuations', icon: 'pi pi-fw pi-check-square', routerLink: ['/valuation']},
            ]
          },
          {label:'Subscribe', icon:'',routerLink:['/corporate/subscriptions']}
        ];
        break;

        case 'valuer':
          this.model = [
            {label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/']},
            {
              label: 'Valuations', icon: 'pi pi-fw pi-star-o', badge: 6,
              items: [
                {label: 'My Instructions', icon: 'pi pi-fw pi-check-square', routerLink: ['/valuer/instructions']},
              ]
            },
            {label:'Subscribe', icon:'',routerLink:['/valuer/subscriptions']}
          ];
          break;

        case '/portal':
          this.model = [
            {label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/']},
            {
              label: 'Valuations', icon: 'pi pi-fw pi-star-o', routerLink: ['/'], badge: 6,
              items: [
                {label: 'Instant Valuation', icon: 'pi pi-fw pi-directions', routerLink: ['/valuation']},
                {label: 'My Valuations', icon: 'pi pi-fw pi-check-square', routerLink: ['/']},
              ]
            },
            {label:'Subscribe', icon:'pi pi-fw pi-wallet',routerLink:['/portal/subscriptions']}
          ];
        break;

      default:
        break;
    }
  }

}
