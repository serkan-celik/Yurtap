import { Component, OnInit } from '@angular/core';
import { KullaniciService } from 'src/app/services/kullanici.service';
import { UserRoleList } from 'src/app/models/account/CurrentUser';
import { ResponseType } from 'src/app/consts/ResponseType';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert/alert.service';
import { IonItemSliding } from '@ionic/angular';

@Component({
  selector: 'app-kullanici-listesi',
  templateUrl: './kullanici-listesi.component.html',
  styleUrls: ['./kullanici-listesi.component.scss'],
})
export class KullaniciListesiComponent implements OnInit {

  constructor(
    private kullaniciservice: KullaniciService,
    private router: Router,
    private alertService: AlertService) { }

  kullaniciRolleriListesi: UserRoleList[] = [];
  filteredkullaniciRolleriListesi: UserRoleList[] = [];
  filterText: string = "";
  veriYok: string = "";

  ngOnInit() {
    this.getKullaniciRolleriListesi();
  }

  ionViewDidEnter() {
    this.getKullaniciRolleriListesi();
  }

  getKullaniciRolleriListesi() {
    this.kullaniciservice.getKullaniciRolleriListesi().subscribe(data => {
      this.kullaniciRolleriListesi = data;
      this.filteredkullaniciRolleriListesi = data;
    }, error => {
      if (error.status == ResponseType.NotFound)
        this.veriYok = "Kullanıcı yok. Eklemek için butona tıklayınız.";
    });
  }

  filteredList() {
    this.kullaniciRolleriListesi = this.filteredkullaniciRolleriListesi.filter(item => {
      return (item.adSoyad).toLocaleLowerCase().indexOf(this.filterText.trim().toLocaleLowerCase()) > -1
    });
  }

  kullaniciEkle() {
    this.router.navigateByUrl("kisi-listesi")
  }

  kullaniciSil(kullanici) {
    this.alertService.confirmDeleteAlert("<b>" + kullanici.adSoyad + "</b> kullanıcı hesabı silinsin mi?", this.kullaniciservice.deleteKullanici(kullanici), this.kullaniciRolleriListesi, kullanici, "Kullanıcı hesabı silindi")
  }

  ogrenciDuzenle(kullanici, itemSliding: IonItemSliding) {
    localStorage.setItem("kullanici", JSON.stringify(kullanici));
    this.router.navigateByUrl("/kullanici-duzenle/" + kullanici.kisiId);
    itemSliding.close();
  }

  hesapGoster(kullanici) {
    this.alertService.advancedAlert("Kullanıcı Hesabı", "<b>Kullanıcı Adı:</b> " + kullanici.kullaniciAd + "<br/><b>Şifre:</b> " + kullanici.sifre);
  }

  yetkiDuzenle(kullanici, itemSliding: IonItemSliding) {
    this.router.navigateByUrl('/yeni-yetki/' + kullanici.kisiId + '/' + kullanici.adSoyad);
    itemSliding.close();
  }
}
