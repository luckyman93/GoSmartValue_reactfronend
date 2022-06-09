import { Component, OnInit } from '@angular/core';
import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {StandardValuationService} from "../../../../services/standard-valuation.service";
import {IInstantValuationReportRequest} from "../../model/interface/instant-valuation";
import {InstantValuationReportRequest} from "../../model/instant-valuation";
import {AuthenticationService} from "../../../../services/authentication.service";
import {Router} from "@angular/router";
import {DialogService} from 'primeng/dynamicdialog';
import {DynamicDialogRef} from 'primeng/dynamicdialog';
import {LoginSignupModalComponent} from "../../../../../auth/components/login-signup-modal/login-signup-modal.component";
import {Product} from "../../../../models/product";
import Swal from "sweetalert2";
import {IBasketProduct} from "../../../../models/interfaces/basket-product";
import {BasketProduct} from "../../../../models/basket-product";
import {BasketService} from "../../../../services/basket.service";

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss'],
  providers: [DialogService]
})
export class SummaryComponent implements OnInit {

  product : any;
  basketItem : IBasketProduct = new BasketProduct();
  instantValuationReportRequest: IInstantValuationReportRequest = new InstantValuationReportRequest();
  submitted: boolean = false;
  displayModal: boolean = false;
  ref: DynamicDialogRef = new DynamicDialogRef();

  constructor(
    public instantValuation: InstantValuationComponent,
    private valuationService: StandardValuationService,
    private authService: AuthenticationService,
    private router: Router,
    private basketService: BasketService,
    public dialogService: DialogService) {
    this.instantValuation.activeIndex = 1
  }

  ngOnInit(): void {
    this.instantValuationReportRequest = this.valuationService.getReportRequest();
  }

  checkout() {
    this.valuationService.saveReportRequest(this.instantValuationReportRequest);
    if(this.authService.isAuthenticated()){

      this.valuationService.createInstantReportRequest(this.instantValuationReportRequest).subscribe(res =>{
        this.basketItem.productId = 1;
        this.basketItem.price = 750;
        this.basketItem.inputData.locationId = this.instantValuationReportRequest.locationId;
        this.basketItem.inputData.localityId = this.instantValuationReportRequest.localityId;
        this.basketItem.inputData.streetName = this.instantValuationReportRequest.streetName;
        this.basketItem.inputData.plotNo = this.instantValuationReportRequest.plotNo;
        this.basketItem.inputData.plotSize = this.instantValuationReportRequest.plotSize;
        this.basketItem.inputData.plotSizeMetric = this.instantValuationReportRequest.metric;
        this.basketItem.inputData.zoning = this.instantValuationReportRequest.landUse;
        this.basketItem.inputData.developmentPhase = this.instantValuationReportRequest.propertyType;

        this.basketService.addToBasket(this.basketItem).subscribe(res => {
          Swal.fire({
            position: 'top-end',
            title: 'Successfully added instant valuation to your Basket!',
            icon: 'success',
            confirmButtonText: 'Ok'
          })
        })

      })
    }else{
      this.openLoginSignUpDialog(true);
    }
  }

  edit() {
    this.router.navigate(["/location-details"]);
  }

  openLoginSignUpDialog(loginOrRegister: boolean) {
    this.ref = this.dialogService.open(LoginSignupModalComponent, {
      header: 'GoSmartValue',
      contentStyle: {"overflow": "auto"},
      baseZIndex: 10000,
      data: {
        loginOrRegister: loginOrRegister
      }
    });
  }

  addToBasket(product: Product) {
    this.product = product;
    this.basketItem.productId = product.productId;
    this.basketItem.price = product.price;

    this.basketService.addToBasket(this.basketItem).subscribe(res => {
      Swal.fire({
        position: 'top-end',
        title: 'Successfully Added ' + product.name + ' to your Basket!',
        icon: 'success',
        confirmButtonText: 'Ok'
      })
    })
  }
}
