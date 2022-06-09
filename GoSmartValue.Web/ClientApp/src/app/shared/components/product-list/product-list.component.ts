import {Component, Input, OnInit} from '@angular/core';
import {ProductService} from "../../services/product.service";
import {IProduct} from "../../models/interfaces/product";

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {

  @Input() typeFilter : number = 1;
  products : IProduct[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(){
    this.productService.getProducts().subscribe(res=>{
      for(var product of res){
        if(product.type == this.typeFilter){
          this.products.push(product);
        }
      }
    })
  }

}
