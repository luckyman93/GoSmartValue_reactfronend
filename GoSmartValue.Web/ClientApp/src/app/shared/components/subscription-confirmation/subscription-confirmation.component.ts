import { Component, OnInit } from '@angular/core';
import {IProduct} from "../../models/interfaces/product";
import {Product} from "../../models/product";
import {DynamicDialogConfig, DynamicDialogRef} from "primeng/dynamicdialog";
import {BasketService} from "../../services/basket.service";
import {IPaymentToken} from "../../models/interfaces/payment-token";
import {PaymentToken} from "../../models/payment-token";

@Component({
  selector: 'app-subscription-confirmation',
  templateUrl: './subscription-confirmation.component.html',
  styleUrls: ['./subscription-confirmation.component.scss']
})
export class SubscriptionConfirmationComponent implements OnInit {

  subscriptionPackage : any;
  product : IProduct = new Product();

  constructor(
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef) { }

  ngOnInit(): void {
    this.subscriptionPackage = this.config.data.package;
  }

}
