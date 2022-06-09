import {Component, Input, OnInit} from '@angular/core';
import {BasketService} from "../../services/basket.service";
import {IProduct} from "../../models/interfaces/product";
import {IBasketProduct} from "../../models/interfaces/basket-product";
import {BasketProduct} from "../../models/basket-product";
import {Product} from "../../models/product";
import Swal from "sweetalert2";
import {AuthenticationService} from "../../services/authentication.service";
import {LoginSignupModalComponent} from "../../../auth/components/login-signup-modal/login-signup-modal.component";
import {DialogService, DynamicDialogRef} from "primeng/dynamicdialog";
import { OrderProductComponent } from '../order-product/order-product.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  @Input() product : any;
  displayReport : boolean = false;
  reportUrl : string = '';
  basketItem : IBasketProduct = new BasketProduct();
  ref: DynamicDialogRef = new DynamicDialogRef();
  width!: string;
  height!: string;

  constructor(private basketService: BasketService,
              private authService: AuthenticationService,
              public dialogService: DialogService) { }

  ngOnInit(): void {
  }

  showSample(sample: string) {
    this.displayReport = true;
    this.reportUrl = sample;
  }
  openLoginSignUpDialog() {
    this.ref = this.dialogService.open(LoginSignupModalComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        loginOrRegister: true,
        basketItem: this.basketItem,
        product: this.product
      }
    });
  }

  openOrderDialog() {
    if((this.product.name !='Estate Report') && (this.product.name != 'Nodal Report') && (this.product.name != 'Deeds Report') )
    {
        this.width = '70rem'
        this.height = '50rem'
    }
    else {
        this.width = '50rem'
        this.height = '40rem'
    }
    this.ref = this.dialogService.open(OrderProductComponent, {
      width:this.width,
      height:this.height,
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        reportType: this.product
      }
    });
  }


  addToBasket(product: Product) {
    this.product = product;
    this.basketItem.productId = product.productId;
    this.basketItem.price = product.price;

    if(this.authService.isAuthenticated()){
      this.basketService.addToBasket(this.basketItem).subscribe(res => {
        Swal.fire({
          position: 'top-end',
          title: 'Successfully added ' + product.name + ' to your Basket!',
          icon: 'success',
          confirmButtonText: 'Ok'
        })
      })
    }else {
      this.openLoginSignUpDialog();
    }
  }
}
