import {Component, Input, OnInit} from '@angular/core';
import {BasketService} from "../../../../services/basket.service";
import Swal from "sweetalert2";

@Component({
  selector: 'app-order-item',
  templateUrl: './order-item.component.html',
  styleUrls: ['./order-item.component.scss']
})
export class OrderItemComponent implements OnInit {

  @Input() productName: string = "";
  @Input() paymentFrequency: string = "";
  @Input() price: string = "";
  @Input() description: string = "";
  @Input() productId: any;
  @Input() basketStatus: any;
  
  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  deleteItems(itemId : any, productName: string){
    this.basketService.removeFromBasket(itemId).subscribe(res => {
      Swal.fire({
        position: 'top-end',
        title: 'Successfully removed ' + productName + ' from your Basket!',
        icon: 'success',
        confirmButtonText: 'Ok'
      });
      this.reload(), 7000;
    })
  }

  reload(){
    window.location.reload();
  }

}
