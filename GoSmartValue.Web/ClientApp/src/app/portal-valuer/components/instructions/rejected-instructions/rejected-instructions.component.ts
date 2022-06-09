import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-val-rejected-instructions',
  templateUrl: './rejected-instructions.component.html',
  styleUrls: ['./rejected-instructions.component.scss']
})
export class RejectedInstructionsComponent implements OnInit {

  instructions: any[] = [];
  columns = [
    {name:"From", value:'issuerName'},
    {name:"City", value:'LocationName'},
    {name:"Client Name",value:'ClientName'},
    {name:"Client Number", value:'ClientMobileNumber'},
    {name:"Plot Number",value:'PlotNumber'},
    {name:"Ward",value:'LocalityName'},
    {name:"Preferred Acceess Date",value:'accessDate'},
    {name:"Rejection Reason",value:'reason'},
  ]

  constructor() { }

  ngOnInit(): void {
  }

}
