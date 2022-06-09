import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, Observable} from "rxjs";
import { catchError } from 'rxjs/operators';
import {environment} from "../../../environments/environment";
import {Configuration} from "../models/configurations";
import {ErrorHandlerService} from "../error-handler/error-handler.service";

@Injectable({
  providedIn: 'root'
})
export class ApiConfigurationService {
  private _config = new BehaviorSubject<Configuration>(new Configuration());
  public obsConfig = this._config.asObservable();

  private API_URL = '';
  private HOST = '';
  private API_KEY = '';
  private FIRE_API_KEY = '';
  private FIRE_AUTH_DOMAIN = '';
  private FIRE_PROJECT_ID = '';
  private FIRE_STORAGE_BUCKETID = '';
  private FIRE_MESSAGING_SENDER_ID = '';
  private FIRE_APP_ID = '';
  private FIRE_MEASUREMENT_ID = '';

  HasValues: boolean = false;
  configUrl: string = environment.apiUrl;

  constructor(
    private _http: HttpClient,
    private errorHandler: ErrorHandlerService
  ) {}

  setConfigValues(config: Configuration) {
    this._config.next(config);
  }

  get hasValues() {
    return this.HasValues;
  }
  get apiUrl() {
    return this.API_URL;
  }
  get host() {
    return this.HOST;
  }
  get apiKey() {
    return this.API_KEY;
  }
  get fireBaseSettings() {
    return {
      apiKey: this.FIRE_API_KEY,
      authDomain: this.FIRE_AUTH_DOMAIN,
      projectId: this.FIRE_PROJECT_ID,
      storageBucket: this.FIRE_STORAGE_BUCKETID,
      messagingSenderId: this.FIRE_MESSAGING_SENDER_ID,
      appId: this.FIRE_APP_ID,
      measurementId: this.FIRE_MEASUREMENT_ID,
    };
  }
  getConfigurations(): Observable<any> {
    return this._http
      .get<any>(`${environment.apiUrl}`)
      .pipe(catchError(this.errorHandler.errorHandler));
  }
}
