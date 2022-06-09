import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-trends-menu',
  templateUrl: './trends-menu.component.html',
  styleUrls: ['./trends-menu.component.scss']
})
export class TrendsMenuComponent implements OnInit {

  public items: any[] = [];

  constructor() { }

  ngOnInit(): void {

    this.items = [
      {label: 'Land', icon: 'pi pi-fw pi-map-marker', routerLink: ['/land-rates']},
      {
        label: 'Building', icon: 'pi pi-fw pi-list',  routerLink: ['/'],
        items: [
          {label: 'Building Rates', icon: 'pi pi-fw pi-star-o', routerLink: ['/building-costs']},
          {label: 'Building Materials', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/building-material-costs']},
        ]
      },
      {label: 'Sales', icon: 'pi pi-fw pi-chart-line', routerLink: ['/sales-trends']},
      {label: 'Rental', icon: 'pi pi-fw pi-info-circle', routerLink: ['/']}

    ];
  }

}
