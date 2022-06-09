import { Component, OnInit } from '@angular/core';
import { IPackage } from 'src/app/shared/models/interfaces/package';
import { ProductService } from 'src/app/shared/services/product.service';

@Component({
  selector: 'app-subscription-list',
  templateUrl: './subscription-list.component.html',
  styleUrls: ['./subscription-list.component.scss']
})
export class SubscriptionListComponent implements OnInit {

  packages!: IPackage[];
  corporatePackages: IPackage[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getPackages();
  }
  getPackages(){
    this.productService.getPackages().subscribe( res => {
      this.packages = res as IPackage[];
      for(let i =0; i < this.packages.length; i++){
        if(this.packages[i].name.includes("Corporate")){
          this.corporatePackages.push(this.packages[i]);
        }
      }
    })
  }

}
