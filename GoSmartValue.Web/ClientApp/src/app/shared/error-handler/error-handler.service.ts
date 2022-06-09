import { Injectable } from '@angular/core';
import {HttpErrorResponse} from "@angular/common/http";
import {throwError} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor() { }

  errorHandler(error: HttpErrorResponse) {
    return throwError(error);
  }
}
