import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-val-completed-instructions',
  templateUrl: './completed-instructions.component.html',
  styleUrls: ['./completed-instructions.component.scss']
})
export class CompletedInstructionsComponent implements OnInit {


  instructions: any[]=[];
  columns = [
    {name:"From", value:'issuerName'},
    {name:"City", value:'LocationName'},
    {name:"Client Name",value:'ClientName'},
    {name:"Client Number", value:'ClientMobileNumber'},
    {name:"Plot Number",value:'PlotNumber'},
    {name:"Ward",value:'LocalityName'},
    {name:"Preferred Acceess Date",value:'accessDate'}
  ]
  constructor() { }

  ngOnInit(): void {
  }

}
