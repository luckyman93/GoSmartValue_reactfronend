import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpResponse} from "@angular/common/http";
import { Observable} from "rxjs";
import { environment} from "../../../environments/environment";
import {ApiConfigurationService} from "./api-configuration.service";
import {IRegister} from "../../auth/models/interfaces/register";
import {IUser} from "../../auth/models/interfaces/user";
import {IResponse} from "../models/interfaces/response";
import {catchError} from "rxjs/operators";
import {ErrorHandlerService} from "../error-handler/error-handler.service";
import {Configuration} from "../models/configurations";
import {Login} from "../../auth/models/login";
import {User} from "../../auth/models/user";
import {IPackage} from "../models/interfaces/package";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  _accountUrl = environment.apiUrl + `/api/accounts`;
  private configApi: Configuration = new Configuration();
  isLoggedIn:boolean = false;

  headerDict = {
    'apiKey': environment.apiKey
  }

  headerDict_loggedIn = {
    'Authorization': "Bearer "+ this.getToken(),
    'apiKey': environment.apiKey
  }

  requestOptions = {
    headers: new HttpHeaders (this.headerDict),
  };

  requestOptions_loggedIn = {
    headers: new HttpHeaders (this.headerDict_loggedIn),
  };

  constructor(
    private http: HttpClient,
    private config: ApiConfigurationService,
    private errorHandler: ErrorHandlerService
  ) {
    this.config.obsConfig.subscribe((x:any) => {
      this.configApi = x;
    });
  }

  standardUserRegistration(userDetails: IRegister): Observable<IResponse<IUser>>{
    return this.http.post<IResponse<IUser>>(`${this._accountUrl}/register`, userDetails, this.requestOptions)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  valuerRegistration(userDetails: IRegister): Observable<IResponse<IUser>>{
    return this.http.post<IResponse<IUser>>(`${this._accountUrl}/register`, userDetails, this.requestOptions)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  getAllUsers(){
    return this.http.get<any[]>(`${this._accountUrl}`, this.requestOptions)
  }

  login(userCredentials: Login): Observable<any> {
    return this.http.post<any>(`${this._accountUrl}/login`, userCredentials, this.requestOptions);
  }

  isRegistered(email: string): Observable<IResponse<any>> {
    return this.http.get<IResponse<any>>(`${this._accountUrl}/${email}/status`, this.requestOptions)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  saveToken(token: string) {
    localStorage.setItem('accessTok', token);
  }

  editProfileDetails(userDetails: any): Observable<any>{
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.getToken(),
        'apiKey': environment.apiKey
      }),
    };
    return this.http.put<any>(`${this._accountUrl}/user/current/editprofile`, userDetails, requestOptions_loggedIn)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  forgotPassword(userName: any): Observable<any>{
    return this.http.put<any>(`api/account/forgot-password`, userName, this.requestOptions)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  changePassword(userDetails: any): Observable<any>{
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.getToken(),
        'apiKey': environment.apiKey
      }),
    };

    return this.http.put<any>(`api/account/reset-password`, userDetails, requestOptions_loggedIn)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  saveUserDetails(user: User) {
    localStorage.setItem('fullname', user.firstName + " " + user.lastName);
    localStorage.setItem('username', user.userName);
    localStorage.setItem('url', user.portalUrl);
  }

  getToken(): string {
    return <string>localStorage.getItem('accessTok');
  }

  getFullname(): string {
    return <string>localStorage.getItem('fullname');
  }

  getUsername(): string {
    return <any>localStorage.getItem('username');
  }

  getUrl(): string {
    return <any>localStorage.getItem('url');
  }

  deleteToken(): void {
    localStorage.removeItem('accessTok');
  }

  deleteFullname(): void {
    localStorage.removeItem('fullname');
  }

  deleteUsername(): void {
    localStorage.removeItem('username');
  }

  deleteUrl(): void {
    localStorage.removeItem('url');
  }

  getUserDetails(token:any): Observable<any> {
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ token,
        'apiKey': environment.apiKey
      }),
    };
    return this.http.get<any>(`${this._accountUrl}/user/current`, requestOptions_loggedIn)
      .pipe(catchError(this.errorHandler.errorHandler));
  }

  getCurrentUser()
  {
    let requestOptions_loggedIn = {
      headers: new HttpHeaders ( {
        'Authorization': "Bearer "+ this.getToken(),
        'apiKey': environment.apiKey
      }),
    };

    return this.http.get<any>(`${this._accountUrl}/user/current`, requestOptions_loggedIn)
    .pipe(catchError(this.errorHandler.errorHandler));
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  logOut():void{

    this.http.get<any>(`${this._accountUrl}/logout`, this.requestOptions_loggedIn).subscribe();
    this.deleteUsername();
    this.deleteFullname();
    this.deleteToken();
    this.deleteUrl();
  }
}
