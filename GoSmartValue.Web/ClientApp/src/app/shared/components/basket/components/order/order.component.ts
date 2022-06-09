import {Component, Input, OnInit} from '@angular/core';
import {Basket} from "../../../../models/basket";
import {IBasket} from "../../../../models/interfaces/basket";

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  @Input() basket : any;
  constructor() { }

  ngOnInit(): void {
  }

}
