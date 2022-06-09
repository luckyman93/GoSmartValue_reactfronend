import { Component, OnInit } from '@angular/core';
import { MarketService } from 'src/app/shared/services/market.service';

@Component({
  selector: 'app-building-material-cost',
  templateUrl: './building-material-cost.component.html',
  styleUrls: ['./building-material-cost.component.scss']
})
export class BuildingMaterialCostComponent implements OnInit {

  buildingMaterials!: any[];
  loading: boolean = true;

  constructor( private marketService: MarketService

  ) { }

  ngOnInit(): void {
    this.getBuildingMaterialCost();
  }

  getBuildingMaterialCost()
  {
    this.marketService.getAllBuildingMaterialCosts().subscribe((data: any) => {
      this.buildingMaterials = data.data;
    });
    this.loading = false;
  }

}
