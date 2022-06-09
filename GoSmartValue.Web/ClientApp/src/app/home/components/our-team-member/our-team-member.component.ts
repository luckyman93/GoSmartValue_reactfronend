import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-our-team-member',
  templateUrl: './our-team-member.component.html',
  styleUrls: ['./our-team-member.component.scss']
})
export class OurTeamMemberComponent implements OnInit {

  @Input() member : any;

  constructor() { }

  ngOnInit(): void {
  }

}
