import { Injectable } from '@angular/core';
import { HttpParams, HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HesapService } from './hesap/hesap.service';

@Injectable({
  providedIn: "root"
})
export class HttpService {
  constructor(private httpClient: HttpClient,private accountService:HesapService) {
  }

  params = new HttpParams();
  options = {
    headers: new HttpHeaders().set(
      "Authorization",
      "Bearer " + this.accountService.getToken
    ),
    params: this.params,
    reportProgress: true,
    responseType: null,
    withCredentials: false,
    body: null
  };

  public getHttp<T>(path: string, functionName: string): Observable<T> {
    this.options.responseType = "json";
    return this.httpClient.get<T>(path + functionName, this.options);
  }

  public postHttp<T>(apiPath: string, functionName: string, body: any, responseType?: any): Observable<T> {
    if (responseType) {
      this.options.responseType = responseType;
    }
    else{
      this.options.responseType = "json";
    }
    return this.httpClient.post<T>(apiPath + functionName, body, this.options);
  }

  public putHttp<T>(apiPath: string, functionName: string, body: any): Observable<T> {
    this.options.responseType = "json";
    return this.httpClient.put<T>(apiPath + functionName, body, this.options);
  }

  public deleteHttp<T>(apiPath: string, functionName: string, body: any): Observable<T> {
    this.options.responseType = "json";
    this.options.body = body;
    return this.httpClient.delete<T>(apiPath + functionName, this.options);
  }
}