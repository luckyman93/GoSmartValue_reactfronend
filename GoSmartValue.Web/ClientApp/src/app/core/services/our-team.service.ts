import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {TeamMember} from "../../shared/models/person.model";
import { Partner } from 'src/app/shared/models/partner';

@Injectable({
  providedIn: 'root'
})
export class OurTeamService {

  constructor(private httpClient : HttpClient) { }

  getTeamMembers(){
    return this.httpClient.get<TeamMember>('assets/data/our-team.json');
  }

  getPartners()
  {
    return this.httpClient.get<Partner>('assets/data/our-partners.json');
  }

}
