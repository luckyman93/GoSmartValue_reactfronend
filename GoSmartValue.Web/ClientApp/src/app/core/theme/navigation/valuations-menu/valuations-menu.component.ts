import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-valuations-menu',
  templateUrl: './valuations-menu.component.html',
  styleUrls: ['./valuations-menu.component.scss']
})
export class ValuationsMenuComponent implements OnInit {

  public items: any[] = [];

  constructor() { }

  ngOnInit(): void {

    this.items = [
      {label: 'Issue Instruction', icon: 'pi pi-fw pi-directions', routerLink: ['/valuation']},

    ];
  }

}
