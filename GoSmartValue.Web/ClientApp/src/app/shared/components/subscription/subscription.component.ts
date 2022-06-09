import {Component, Input, OnInit} from '@angular/core';
import {IPackageFeature} from "../../models/interfaces/packageFeature";
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { SubscriptionFeaturesComponent } from '../subscription-features/subscription-features.component';
import {Product} from "../../models/product";
import Swal from "sweetalert2";
import {IBasketProduct} from "../../models/interfaces/basket-product";
import {BasketProduct} from "../../models/basket-product";
import {BasketService} from "../../services/basket.service";
import {AuthenticationService} from "../../services/authentication.service";
import {LoginSignupModalComponent} from "../../../auth/components/login-signup-modal/login-signup-modal.component";
import {SubscriptionConfirmationComponent} from "../subscription-confirmation/subscription-confirmation.component";
import {IPaymentToken} from "../../models/interfaces/payment-token";
import {PaymentToken} from "../../models/payment-token";


@Component({
  selector: 'app-subscription',
  templateUrl: './subscription.component.html',
  styleUrls: ['./subscription.component.scss']
})
export class SubscriptionComponent implements OnInit {

  @Input() package : any;
  @Input() description : string = '';
  @Input() price : number = 0;
  @Input() features : any[] = [];
  @Input() isConfirm : boolean = false;
  classToUse : string = 'standard';
  basketItem : IBasketProduct = new BasketProduct();
  ref: DynamicDialogRef = new DynamicDialogRef();
  paymentToken : IPaymentToken = new PaymentToken();

  constructor(private basketService: BasketService,
              private authService: AuthenticationService,
              private dialogService: DialogService) {
  }

  ngOnInit(): void {
    this.classToUse =  this.description.toLowerCase();
  }

  viewAll(description: string,features:any,price: number)
  {
      const dialogRef = this.dialogService.open(SubscriptionFeaturesComponent,<DynamicDialogConfig>{
        width: '55vw',
        data:{description,features,price}
      });
  }

  addToBasket() {
    this.basketItem.productId = this.package.productId;
    this.basketItem.price = this.package.price;

    if(this.authService.isAuthenticated()){
      this.basketService.addToBasket(this.basketItem).subscribe(res => {
        Swal.fire({
          position: 'top-end',
          title: 'Successfully added ' + this.package.name + ' to your Basket!',
          icon: 'success',
          confirmButtonText: 'Ok'
        })
      })
    }else {
      this.openLoginSignUpDialog();
    }
  }

  openLoginSignUpDialog() {
    this.ref = this.dialogService.open(LoginSignupModalComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        loginOrRegister: true,
        basketItem: this.basketItem,
        product: this.package
      }
    });
  }

  openConfirmationDialog() {
    this.ref = this.dialogService.open(SubscriptionConfirmationComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        package: this.package
      }
    });
  }

  getPaymentToken(){
    this.basketService.getPaymentToken().subscribe(res => {
      this.paymentToken= res;
      console.log("token => ", res)
      var accessKey="$2a$08$huiQpM3HvQwOOlFho00IKek9dN7pk8Ni6U1aNJMcpefmYpgeqqFMW";
      window.location.href = "https://developer.tingg.africa/checkout/v2/express/?accessKey=" + accessKey + "&params=" + this.paymentToken.token + "&countryCode=BW";

    })
  }

}
