import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-completed-instructions',
  templateUrl: './completed-instructions.component.html',
  styleUrls: ['./completed-instructions.component.scss']
})
export class CompletedInstructionsComponent implements OnInit {


  instructions: any[]=[];
  columns = [
    {name:"City", value:'locationName'},
    {name:"Client Name",value:'clientName'},
    {name:"Client Number", value:'clientNumber'},
    {name:"Plot Number",value:'plotNumber'},
    {name:"Ward",value:'localityName'},
    {name:"Valuer",value:'valuerName'},
    {name:"Preferred Acceess Date",value:'accessDate'}
  ]
  constructor() { }

  ngOnInit(): void {
  }

}
