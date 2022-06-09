import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-report-sample',
  templateUrl: './report-sample.component.html',
  styleUrls: ['./report-sample.component.scss']
})
export class ReportSampleComponent implements OnInit {

  @Input() displayReport: boolean = true;
  @Input() sampleUrl: string = '';
  @Input() reportType: string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
