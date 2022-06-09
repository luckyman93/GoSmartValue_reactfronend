import { Component, Input, OnInit } from '@angular/core';
import {ReportService} from "src/app/shared/services/report.service";
import {Router} from "@angular/router";
// import {InstantValuationComponent} from "../../instant-valuation/instant-valuation.component";
import {LocationService} from "src/app/shared/services/location.service";
import {ILocation} from "src/app/shared/models/interfaces/location";
import {IDetailedValuationReportRequest} from "src/app/shared/components/valuation/model/interface/detailed-valuation";
import {ILocality} from "src/app/shared/models/interfaces/locality";
import {AbstractControl, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {StandardValuationService} from "src/app/shared/services/standard-valuation.service";
import {developmentStage} from "src/app/shared/constants/constants";
import {Locality} from "src/app/shared/models/locality";
import {MessageService} from 'primeng/api';
import {DialogService, DynamicDialogConfig, DynamicDialogRef} from 'primeng/dynamicdialog';
import { OrderProduct } from '../../models/order-product';
import { AuthenticationService } from '../../services/authentication.service';
import { IOrderProduct } from '../../models/interfaces/order-report';
import { ProductService } from '../../services/product.service';
import {Product} from "../../models/product";
import Swal from "sweetalert2";
import {BasketService} from "../../services/basket.service";
import {IBasketProduct} from "../../models/interfaces/basket-product";
import {BasketProduct} from "../../models/basket-product";
import {LoginSignupModalComponent} from "../../../auth/components/login-signup-modal/login-signup-modal.component";
import { Location } from "../../models/location";

@Component({
  selector: 'app-order-product',
  templateUrl: './order-product.component.html',
  styleUrls: ['./order-product.component.scss']
})
export class OrderProductComponent implements OnInit {

  @Input() reportType: any;

  product : any;
  title!: string;
  description!: string;

  estates: any[] = [];
  selectedEstates: any[] = [];

  productOrderDetails: IOrderProduct = new OrderProduct;
  username!: string;
  user: any;

  detailedValuationReportRequest!: IDetailedValuationReportRequest;
  submitted: boolean = false;
  locations: ILocation[] = [];
  localities: ILocality[] = [];
  companies: any[] = [];
  developmentStages = developmentStage;
  instantValuationForm: FormGroup = new FormGroup({});
  basketItem : IBasketProduct = new BasketProduct();

  constructor(private reportService: ReportService,
              private router: Router,
              private locationService: LocationService,
              private valuationService: StandardValuationService,
              private fb: FormBuilder,
              private messageService: MessageService,
              public config: DynamicDialogConfig,
              public ref: DynamicDialogRef,
              private authService: AuthenticationService,
              private productService: ProductService,
              private basketService: BasketService,
              public dialogService: DialogService) {

          config.closable = true;
          config.closeOnEscape = true;
          this.title = this.config.data.reportType.name
          this.description = this.config.data.reportType.description
  }

  ngOnInit(): void {
    this.getLocations();
    this.initForm();
    this.getCurrentUser();
    this.detailedValuationReportRequest = this.valuationService.getDetailedRequest();
    this.product = this.config.data.reportType;
    this.basketItem.productId = this.product.productId;
    this.basketItem.inputData = {
      "locationId": 0,
        "localityId": 0,
        "location": new Location(),
        "streetId": 0,
        "streetName": "",
        "plotNo": "",
        "plotSize": 0,
        "plotSizeMetric": 0,
        "zoning": 0,
        "developmentPhase": 0
    }
  }

  private initForm(){
    this.instantValuationForm = this.fb.group({
      location: ['', [Validators.required]],
      localityId: ['', [Validators.required]],
      streetName: ['', [Validators.required]],
      plotNo: ['', [Validators.required]],
      plotSize: ['', Validators.required]
    })
  }

  get f(): { [key: string]: AbstractControl } { return this.instantValuationForm.controls; }

  getLocations(){
    this.locationService.GetLocations().subscribe(res => {
      this.locations = res;
    })

  }

  getLocalities(){
    this.locationService.GetLocalities(this.productOrderDetails.location).subscribe(res => {
      // if(this.productOrderDetails.localityId > 0) {
        this.localities = [];
        if (this.productOrderDetails.locality.locationId != this.productOrderDetails.location.id) {
          this.productOrderDetails.locality = new Locality();
        }
      // }
      this.localities = res;
    })
  }

  getCurrentUser()
  {
    this.authService.getCurrentUser().subscribe(res => {
      this.user = res
      this.username = this.user.data.userName;
    });
  }

  // placeOrder()
  // {
  //   this.productOrderDetails.email = this.username;
  //   console.log("SUBMITTED",this.productOrderDetails);
  //   this.productService.sendToZoho(this.productOrderDetails).subscribe(res =>{
  //     console.log('Response',res)
  //   },
  //   err =>{
  //       console.log('Error',err)
  //   } );
  // }

  placeOrder() {
    console.log("my item ==> ", this.basketItem)
    console.log("my reportType ==> ", this.product)
    console.log("my this.productOrderDetails ==> ", this.productOrderDetails.location.id)

    if(this.productOrderDetails.location.id != null || this.productOrderDetails.location.id != 0) {
      this.basketItem.inputData.locationId = this.productOrderDetails.location.id
      this.basketItem.inputData.location = this.productOrderDetails.location
      console.log("my item ==> ", this.basketItem)
    }
    if(this.productOrderDetails.locality.id != null || this.productOrderDetails.locality.id != 0){
      this.basketItem.inputData.localityId = this.productOrderDetails.locality.id
    }
    this.basketItem.productId = this.product.productId;
    this.basketItem.price = this.product.price;

    if(this.authService.isAuthenticated()){
      this.basketService.addToBasket(this.basketItem).subscribe(res => {
        Swal.fire({
          position: 'top-end',
          title: 'Successfully added ' + this.product.name + ' to your Basket!',
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
        product: this.reportType
      }
    });
  }

}
