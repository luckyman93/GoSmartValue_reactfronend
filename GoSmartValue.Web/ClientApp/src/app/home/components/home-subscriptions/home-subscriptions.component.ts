import { Component, OnInit } from '@angular/core';
import {IPackage} from "../../../shared/models/interfaces/package";
import {ProductService} from "../../../shared/services/product.service";

@Component({
  selector: 'app-home-subscriptions',
  templateUrl: './home-subscriptions.component.html',
  styleUrls: ['./home-subscriptions.component.scss']
})
export class HomeSubscriptionsComponent implements OnInit {

  packages : IPackage[] = [];
  standardPackages : IPackage[] = [];
  valuerPackages : IPackage[] = [];
  corporatePackages : IPackage[] = [];
  superPackage : any = [];
  packageTypes: any[] = [];

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
      this.superPackage.push(this.standardPackages);
      this.superPackage.push(this.valuerPackages);
      this.superPackage.push(this.corporatePackages);
    })

    this.packageTypes = [
      {title: 'Standard',
        packages: this.standardPackages}, 
      {title: 'Valuer',
        packages: this.valuerPackages},
      {title: 'Corporate',
      packages: this.corporatePackages}];
  }

  

}
