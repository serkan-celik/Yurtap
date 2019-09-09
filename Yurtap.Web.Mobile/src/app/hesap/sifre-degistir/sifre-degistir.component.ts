import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/BaseComponent';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { User } from 'src/app/models/account/User';
import { NgForm } from '@angular/forms';
import { KullaniciService } from 'src/app/services/kullanici.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { Platform } from '@ionic/angular';

@Component({
  selector: 'app-sifre-degistir',
  templateUrl: './sifre-degistir.component.html',
  styleUrls: ['./sifre-degistir.component.scss'],
})
export class SifreDegistirComponent extends BaseComponent implements OnInit {

  constructor(
    public hesapService: HesapService,
    private kullaniciservice: KullaniciService,
    private toastService: ToastService,
    public platform: Platform) {
    super(hesapService);
  }

  kullanici = new User();
  kullaniciAd: boolean = false;

  ngOnInit(): void {
    this.kullanici.kisiId = this.currentUser.id;
    this.kullanici.ad = this.currentUser.name;
  }

  sifreKaydet(form: NgForm) {
    this.kullaniciservice.updateKullanici(this.kullanici).subscribe(data => {
      if (data)
        this.toastService.showToast("Şifreniz başarıyla değiştirildi")
    }, error => {
      this.toastService.showToast(error.error);
    });
  }
}
