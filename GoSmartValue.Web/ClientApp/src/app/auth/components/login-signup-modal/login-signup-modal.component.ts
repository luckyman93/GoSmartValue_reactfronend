import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import {IBasketProduct} from "../../../shared/models/interfaces/basket-product";
import {BasketProduct} from "../../../shared/models/basket-product";
import {F} from "@angular/cdk/keycodes";

@Component({
  selector: 'app-login-signup-modal',
  templateUrl: './login-signup-modal.component.html',
  styleUrls: ['./login-signup-modal.component.scss']
})
export class LoginSignupModalComponent implements OnInit {

  @Input() display: boolean = true;
  @Input() selected: boolean = false;
  basketItem : IBasketProduct = new BasketProduct();
  product: any;

  constructor(
    public config: DynamicDialogConfig,
    public ref: DynamicDialogRef) {

    config.closable = true;
    config.closeOnEscape = true;
  }

  ngOnInit(): void {
    this.selected = this.config.data.loginOrRegister;
    this.basketItem = this.config.data.basketItem;
  }

  closeDialog(event: any)
  {
      this.ref.close();
  }
}
