import { Injectable } from '@angular/core';
import {Subject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  private paymentComplete = new Subject<any>();
  paymentComplete$ = this.paymentComplete.asObservable();
  constructor() { }
}
