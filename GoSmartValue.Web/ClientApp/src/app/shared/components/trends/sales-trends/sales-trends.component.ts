import { Component, OnInit } from '@angular/core';

import { MarketService } from 'src/app/shared/services/market.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LocationService } from 'src/app/shared/services/location.service';
import { ILocation } from 'src/app/shared/models/interfaces/location';
import { ILocality } from 'src/app/shared/models/interfaces/locality';
import { formatDate } from "@angular/common";
import {MessageService} from 'primeng/api';
import { sampleTime } from 'rxjs/operators';


@Component({
  selector: 'app-sales-trends',
  templateUrl: './sales-trends.component.html',
  styleUrls: ['./sales-trends.component.scss']
})
export class SalesTrendsComponent implements OnInit {

  
  salesData: any;
  dataValues: any[] = [];
  chartlabels: any[] = [];
  chartValues: any[] = [];

  dateFrom!: Date;
  dateTo!: Date;
  location!: ILocation;
  locality!: ILocality;

  locations: ILocation[] = [];
  localities: ILocality[] = [];
  trendInfo: any;
  

  constructor(private marketService: MarketService,
              private locationService: LocationService,
              private messageService: MessageService) { }

  ngOnInit(): void {
    this.getLocations();
    
  }

  prePopulateGraph(){

    const formattedFromDate = formatDate('01-01-2018','dd/MM/yyyy','en-US');
    const formattedToDate = formatDate( '01/01/2021','dd/MM/yyyy','en-US');

    let sampleLocation = this.locations.find( element => element.id == 33); 
    this.location = sampleLocation!
    
    this.trendInfo = {

      FromDate: formattedFromDate,
      ToDate: formattedToDate,
      LocationId:  this.location.id,
      Location:  this.location.name

    }

    this.marketService.getSalesTrends(this.trendInfo).subscribe((res: any) => {
      this.salesData = res.data;
    
      this.salesData.forEach((element: any) => {

        let saleTrend = {
          Year:element.key.slice(0,4),
          Month:element.key.slice(5,7),
          value: element.value
        }
        this.dataValues.push(saleTrend);
      });
      
      this.dataValues.sort((a,b) => a.Month - b.Month);
      this.sortData()
    
    }, (err)=>{
      this.messageService.add({key: 'tc', severity:'error', summary: 'Error', detail: 'We have encountered a problem'});
    });
    
  }

  getLocations(){
    this.locationService.GetLocations().subscribe(res => {
      this.locations = res;
      this.prePopulateGraph();
    })
    
  }

  
  getSalesData()
  {
    const formattedFromDate = formatDate(this.dateFrom,'dd/MM/yyyy','en-US');
    const formattedToDate = formatDate(this.dateTo,'dd/MM/yyyy','en-US');
    
    this.trendInfo = {

      FromDate: formattedFromDate,
      ToDate: formattedToDate,
      LocationId: this.location.id,
      Location: this.location.name

    }
    
    this.marketService.getSalesTrends(this.trendInfo).subscribe((res: any) => {
      this.salesData = res.data;
      
      this.salesData.forEach((element: any) => {

        let saleTrend = {
          Year:element.key.slice(0,4),
          Month:element.key.slice(5,7),
          value: element.value
        }
        this.dataValues.push(saleTrend);
      });
      
      this.dataValues.sort((a,b) => a.Month - b.Month);
      this.sortData()
    
    }, (err)=>{
      this.messageService.add({key: 'tc', severity:'error', summary: 'Error', detail: 'We have encountered a problem'});
    });
    
  }




  sortData()
  {
    
    this.dataValues.forEach((element:any) => {

      switch (element.Month) {
        case '01':
          element.name = 'Jan' + element.Year
          break;

          case '02':
            element.name = 'Feb' + element.Year
            break;
            case '03':
          element.name = 'Mar' + element.Year
          break;

          case '04':
          element.name = 'Apr' + element.Year
          break;

          case '05':
          element.name = 'May' + element.Year
          break;

          case '06':
          element.name = 'Jun' + element.Year
          break;

          case '07':
          element.name = 'Jul' + element.Year
          break;

          case '08':
          element.name = 'Aug' + element.Year
          break;

          case '09':
          element.name = 'Sep' + element.Year
          break;

          case '10':
          element.name = 'Oct' + element.Year
          break;

          case '11':
          element.name = 'Nov' + element.Year
          break;

          case '12':
          element.name = 'Dec' + element.Year
          break;
      
        default:
          break;
      }
      this.chartlabels.push(element.name);
      this.chartValues.push(element.value);

      });


      this.setGraphData()
      
  }


  setGraphData()
  {

    this.salesData = {
      labels: this.chartlabels,
      datasets: [
          {
              label: this.location.name,
              data: this.chartValues,
              fill: false,
              borderColor: '#42A5F5',
              tension: .4
           }
      ]
    };
  }

}
