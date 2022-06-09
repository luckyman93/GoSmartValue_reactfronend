import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree} from '@angular/router';
import { Observable } from 'rxjs';
import {AuthenticationService} from "../../shared/services/authentication.service";


@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  constructor(private authenticationService: AuthenticationService, private router: Router) {}

  async canActivate(): Promise<boolean> {
    try{
      const authenticated = this.authenticationService.isAuthenticated();

      if(!authenticated){
        this.router.navigate(['/']);
      }

      return true;
    }catch {
      return false;
    }
  }

  private isUserLoggedIn(): boolean {
    // if (this.authenticationService.isUserLoggedIn()) {
    //   return true;
    // }
    this.router.navigate(['/login']);
    // this.notificationService.notify(NotificationType.ERROR, `You need to log in to access this page`);
    return false;
  }

}
