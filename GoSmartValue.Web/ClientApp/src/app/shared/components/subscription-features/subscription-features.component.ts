import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef, DialogService } from 'primeng/dynamicdialog';


@Component({
  selector: 'app-subscription-features',
  templateUrl: './subscription-features.component.html',
  styleUrls: ['./subscription-features.component.scss']
})
export class SubscriptionFeaturesComponent implements OnInit {

  description : string = '';
  price : number = 0;
  features : any[] = [];
  data!: any;
  classToUse : string = 'standard';
  displayModal:boolean= true;
  constructor( private dialogConfig: DynamicDialogConfig ) { }

  ngOnInit(): void {
    this.initData();
  }

  initData()
  {
    this.data = this.dialogConfig.data;
    this.description = this.data.description;
    this.features = this.data.features;
    this.price = this.data.price;

    this.classToUse =  this.description.toLowerCase();
  }

}
