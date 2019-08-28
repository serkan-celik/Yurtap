import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HesapService } from 'src/app/services/hesap/hesap.service';

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

  user: any = {ad:"",sifre:""};
  redirectUrl: string = "ana-sayfa"

  ngOnInit() {
    if (this.accountService.isLoggedIn)
      this.router.navigateByUrl(this.redirectUrl)
    else
      this.activatedRoute.queryParams.subscribe(params => {
        if (params["returnUrl"])
          this.redirectUrl = params["returnUrl"]
      })
  }

  login() {
    this.accountService.login(this.user, this.redirectUrl);
  }
}
