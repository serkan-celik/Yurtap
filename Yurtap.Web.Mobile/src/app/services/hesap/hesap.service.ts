import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { User } from 'src/app/models/account/User';
import { ToastService } from '../toast/toast.service';
import { CurrentUser } from 'src/app/models/account/CurrentUser';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HesapService {

  constructor(
    private httpClient: HttpClient,
    private router: Router, private toastService: ToastService
  ) {
  }
  //userToken: any;
  //decodedToken: any;
  jwtHelper: JwtHelper = new JwtHelper();
  TOKEN_KEY = "yurtap_yoklama_token";

  login(user: User, redirectUrl: string) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient.post(environment.kullaniciPath + "giris", user, { headers: headers })
      .subscribe(data => {
        this.saveToken(data)
        //this.userToken = data;
        //this.decodedToken = this.jwtHelper.decodeToken(data.toString())
        this.toastService.showToast("Sisteme giriş yapıldı")
        this.router.navigateByUrl(redirectUrl)
      })
  }

  logOut() {
    localStorage.removeItem(this.TOKEN_KEY)
    this.router.navigateByUrl("hesap/giris");
    this.toastService.showToast("Sistemden çıkış yapıldı")
  }

  get isLoggedIn() {
    return tokenNotExpired(this.TOKEN_KEY)//tokenın süresi dolmamış ise = true
  }

  saveToken(token) {
    localStorage.setItem(this.TOKEN_KEY, token) // token kaydet
  }

  get getToken() {
      return localStorage.getItem(this.TOKEN_KEY); // token getir
  }

  get getDecodeToken() {
      return this.jwtHelper.decodeToken(this.getToken); // token çöz
  }

  isExpired() {
      return (this.jwtHelper.getTokenExpirationDate(this.getToken) < new Date()) //tokenın süresi dolmuşmu
  }

  getCurrentUser() {
    if(this.isLoggedIn){
    var currentUser = new CurrentUser();
    currentUser.id = this.getDecodeToken.id;
    currentUser.name = this.getDecodeToken.name;
    currentUser.password = this.getDecodeToken.password;
    currentUser.fullName = this.getDecodeToken.fullName;
    currentUser.isLogged = this.isLoggedIn;
    currentUser.roles = JSON.parse(this.getDecodeToken.roles);
    return currentUser;
    }
  }
}
