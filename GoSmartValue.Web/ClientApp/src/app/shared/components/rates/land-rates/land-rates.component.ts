import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LocationService } from 'src/app/shared/services/location.service';
import { MarketService } from 'src/app/shared/services/market.service';

@Component({
  selector: 'app-land-rates',
  templateUrl: './land-rates.component.html',
  styleUrls: ['./land-rates.component.scss']
})
export class LandRatesComponent implements OnInit {

  
  landRates!: any[];
  loading: boolean = true;

  constructor(private http: HttpClient,
              private locationService: LocationService,
              private marketService: MarketService
              ) {}

  ngOnInit(): void {
    this.getLandRates();
  }

  getLandRates(){
    this.marketService.getLandRates().subscribe((rates: any) => {
      this.landRates = rates.data;
    });
    this.loading = false;
  }
}

