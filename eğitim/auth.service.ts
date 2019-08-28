import { Injectable } from "@angular/core";
import { LoginUser } from "../model/LoginUser";
import { HttpHeaders, HttpClient, HttpParams } from "@angular/common/http";
import { JwtHelper, tokenNotExpired } from "angular2-jwt";
import { Router } from "@angular/router";
import { AlertifyService } from "./alertify.service";
import { environment } from "src/environments/environment";
import { NewPasswordView } from "../model/NewPasswordView";
import { Observable } from "rxjs";
import { UserInfoView } from "../model/UserInfoView";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    private router: Router,
    private alertify: AlertifyService
  ) {}
  path = environment.authPath;
  TOKEN_KEY = "token";
  userToken: any;
  decodedToken: any;
  jwtHelper: JwtHelper = new JwtHelper();

  login(loginUser: LoginUser) {
    let headers = new HttpHeaders();
    headers.append("Content-Type", "application/json");
    debugger;
    this.httpClient
      .post(environment.authPath + "loginUser", loginUser, { headers })
      .subscribe(data => {
        this.saveToken(data);
        this.userToken = data;
        this.decodedToken = this.jwtHelper.decodeToken(data.toString());
        this.alertify.success("Sisteme Giriş Yapıldı");
        this.router.navigateByUrl("/dashboard");
      });
  }
  changePassword(newPassword:NewPasswordView)
  {
    return  this.postHttp("changePassword", newPassword);

  }

  saveToken(token) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  logOut() {
    localStorage.removeItem(this.TOKEN_KEY);
  }

  loggedIn() {
    return tokenNotExpired(this.TOKEN_KEY);
  }

  get token() {
    return localStorage.getItem(this.TOKEN_KEY);
  }
  getCurrentUserId() {
    return this.jwtHelper.decodeToken(this.token).nameid;
  }
  getCurrentUserDomain() {
    return this.jwtHelper.decodeToken(this.token).Domain;
  }
  getCurrentUser() {
    return this.jwtHelper.decodeToken(this.token);
  }
  getUserInfo(): Observable<UserInfoView> {
    return this.getHttp("GetUserInfo");
  }

  private postHttp<T>(functionName: string, param: any): Observable<T> {
    let params = new HttpParams();

    const options = {
      headers:   new HttpHeaders().set(
        
        "Authorization",
        "Bearer " + localStorage.getItem("token")
      ),
      params: params,
      reportProgress: true,
      withCredentials: false
    };

    return this.httpClient.post<T>(
      environment.authPath + functionName,
      param,
      options
    );
  }
  
  private getHttp<T>(functionName: string): Observable<T> {
    let params = new HttpParams();

    const options = {
      headers: new HttpHeaders().set(
        "Authorization",
        "Bearer " + localStorage.getItem("token")
      ),
      params: params,
      reportProgress: true,
      withCredentials: false
    };
    return this.httpClient.get<T>(this.path + functionName, options);
  }

}
