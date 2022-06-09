import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import {BasketService} from "../../../../services/basket.service";
import {IBasket} from "../../../../models/interfaces/basket";
import {Basket} from "../../../../models/basket";
import {TermsAndConditionsComponent} from "src/app/shared/components/terms-and-conditions/terms-and-conditions.component";
import {AuthenticationService} from "../../../../services/authentication.service";
import {DialogService} from 'primeng/dynamicdialog';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import Swal from 'sweetalert2'
import {IPaymentToken} from "../../../../models/interfaces/payment-token";
import {PaymentToken} from "../../../../models/payment-token";
import {Router} from "@angular/router";

@Component({
  selector: 'app-shopping-basket',
  templateUrl: './shopping-basket.component.html',
  styleUrls: ['./shopping-basket.component.scss']
})
export class ShoppingBasketComponent implements OnInit {

  currentBasket : IBasket = new Basket();
  basketForStatus: any;
  termsAndConditions : boolean = false;
  ref: DynamicDialogRef = new DynamicDialogRef();
  @Output() closeDialog = new EventEmitter<boolean>();
  user: any;
  paymentToken : IPaymentToken = new PaymentToken();

  constructor(private basketService : BasketService,
              public dialogService: DialogService,
              private authService: AuthenticationService,
              private router: Router
              ) { }

  ngOnInit(): void {
    this.getCurrentBasket();
    this.getUserDetails();
  }

  getCurrentBasket(){
    this.basketService.getCurrentBasket().subscribe( res => {
      this.currentBasket = res;
      this.basketForStatus = this.currentBasket
      if(this.currentBasket.status == 2){
        this.router.navigate(["/order-history"])
      }
    })
  }

  openTerms()
  {
    this.ref = this.dialogService.open(TermsAndConditionsComponent, {
      header: 'Terms and Conditions',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10001,
    });
  }

  confirmBasket()
  {
    this.basketService.confirmBasket(this.user.userId).subscribe ( res =>
      {
        Swal.fire({
          position: 'top-end',
          title: 'Order Confirmed!',
          icon: 'success',
          confirmButtonText: 'Ok'
        })
        this.getCurrentBasket();
      });

  }

  getUserDetails(){
    this.authService.getUserDetails(this.authService.getToken()).subscribe(res => {
      this.user= res.data;
    })
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
