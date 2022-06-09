import { Component, OnInit } from '@angular/core';
import {OurTeamService} from "../../../core/services/our-team.service";
import {TeamMember} from "../../../shared/models/person.model";

@Component({
  selector: 'app-our-team',
  templateUrl: './our-team.component.html',
  styleUrls: ['./our-team.component.scss']
})
export class OurTeamComponent implements OnInit {

  teamMembers? : TeamMember | any;
  numVisible = 0;

  responsiveOptions = [
    {
      breakpoint: '1024px',
      numVisible: 3,
      numScroll: 3
    },
    {
      breakpoint: '768px',
      numVisible: 2,
      numScroll: 2
    },
    {
      breakpoint: '560px',
      numVisible: 1,
      numScroll: 1
    }
  ];

  constructor(private ourTeamService : OurTeamService) { }

  ngOnInit(): void {
    this.getTeamMembers();
  }

  getTeamMembers(){
    this.ourTeamService.getTeamMembers().subscribe( result => {
        this.teamMembers = result;
      }
    )
  }

}
