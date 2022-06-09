import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-heading-description',
  templateUrl: './heading-description.component.html',
  styleUrls: ['./heading-description.component.scss']
})
export class HeadingDescriptionComponent implements OnInit {

  @Input() heading:string='';
  @Input() description?:string='';

  constructor() { }

  ngOnInit(): void {
  }

}
