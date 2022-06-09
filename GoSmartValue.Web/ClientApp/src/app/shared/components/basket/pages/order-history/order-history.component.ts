import { Component, OnInit } from '@angular/core';
import {BasketService} from "../../../../services/basket.service";

@Component({
  selector: 'app-order-history',
  templateUrl: './order-history.component.html',
  styleUrls: ['./order-history.component.scss']
})
export class OrderHistoryComponent implements OnInit {

  allBaskets : any;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.getAllBasket();
  }

  getAllBasket(){
    this.basketService.getAllBaskets().subscribe( res => {
      this.allBaskets = res;
    })
  }

}
