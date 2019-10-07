import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { User } from 'src/app/models/account/User';
import { IonButton, IonText, IonSkeletonText, IonLabel } from '@ionic/angular';

@Component({
  selector: 'app-giris',
  templateUrl: './giris.component.html',
  styleUrls: ['./giris.component.scss'],
})
export class GirisComponent implements OnInit {

  constructor(
    private accountService: HesapService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  user: User = new User();
  redirectUrl: string = "ana-sayfa"
  @ViewChild('loginBtn')
  private loginBtn: IonButton;
  @ViewChild('span')
  private span: ElementRef;

  ngOnInit() {
    if (this.accountService.isLoggedIn)
      this.router.navigateByUrl(this.redirectUrl)
    else
      this.activatedRoute.queryParams.subscribe(params => {
        if (params["returnUrl"])
          this.redirectUrl = params["returnUrl"]
      })
  }

  ionViewWillEnter() {
    if (!this.accountService.isLoggedIn) {
      this.loginBtn.disabled = false;
      this.span.nativeElement.innerHTML = "Giriş Yap";
    }
  }

  login() {
    this.loginBtn.disabled = true;
    this.span.nativeElement.innerHTML = "Giriş yapılıyor..."
    this.accountService.login(this.user, this.redirectUrl, this.loginBtn, this.span);
  }
}
