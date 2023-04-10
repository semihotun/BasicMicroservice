import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from "rxjs";
import { catchError } from 'rxjs/operators';
import { TokenLocalStorage } from '../services/token-local-storage.service';
import { AlertifyService } from '../services/alertify.service';


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(
    private localStorage: TokenLocalStorage,
    private alertifyService: AlertifyService
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.localStorage.getToken();
    let newReq: HttpRequest<any>;
    if (!token) {
      newReq = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${token}`
        }
      });
    }
    else {
      newReq = request.clone();
    }
    return next.handle(newReq).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          errorMessage = `Error: ${error.error.message}`; // client-side error         
        } else {
          errorMessage = error.error;// server-side error     
        }
        this.alertifyService.error(errorMessage);
        return throwError(errorMessage);
      })
    )

  }
}
