import {AfterViewInit, Component, OnDestroy, OnInit, Renderer2} from '@angular/core';
import {PrimeNGConfig} from "primeng/api";
import {AppComponent} from "../../../../app.component";

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent {


  constructor(public appM : AppComponent) {}

  ngOnInit(): void {
  }

}
