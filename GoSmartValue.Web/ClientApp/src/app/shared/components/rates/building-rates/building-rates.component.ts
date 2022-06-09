import { Component, OnInit } from '@angular/core';
import { MarketService } from 'src/app/shared/services/market.service';

@Component({
  selector: 'app-building-rates',
  templateUrl: './building-rates.component.html',
  styleUrls: ['./building-rates.component.scss']
})
export class BuildingRatesComponent implements OnInit {

  buildingCosts!: any[];

  constructor(private marketService: MarketService

  ) { }

  ngOnInit(): void {
    this.getBuildingRates()
  }

  getBuildingRates()
  {
    this.marketService.getBuildingCostRates().subscribe((data:any)=> {
      this.buildingCosts = data.data;
    });
  }

}
