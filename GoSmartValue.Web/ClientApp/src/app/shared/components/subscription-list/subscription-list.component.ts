import { Component, OnInit } from '@angular/core';
import {ProductService} from "../../services/product.service";
import {IPackage} from "../../models/interfaces/package";

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss']
})
export class SubscriptionListComponent implements OnInit {

  packages : IPackage[] = [];
  standardPackages : IPackage[] = [];
  valuerPackages : IPackage[] = [];
  corporatePackages : IPackage[] = [];

  constructor(private productService : ProductService) { }

  ngOnInit(): void {
    this.getPackages();
  }

  getPackages(){
    this.productService.getPackages().subscribe( res => {
      this.packages = res as IPackage[];
      for(let i =0; i < this.packages.length; i++){
        if(this.packages[i].name.includes("Standard")){
          this.standardPackages.push(this.packages[i]);
        }else if(this.packages[i].name.includes("Valuer")){
          this.valuerPackages.push(this.packages[i]);
        }else{
          this.corporatePackages.push(this.packages[i]);
        }
      }
    })
  }

}
