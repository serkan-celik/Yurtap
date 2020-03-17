import { Component, OnInit } from '@angular/core';
import { KullaniciService } from 'src/app/services/kullanici.service';
import { UserRoleList } from 'src/app/models/account/CurrentUser';
import { ResponseType } from 'src/app/consts/ResponseType';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert/alert.service';
import { IonItemSliding } from '@ionic/angular';
import { Kisi } from 'src/app/models/Kisi';

@Component({
  selector: 'app-kisi-listesi',
  templateUrl: './kisi-listesi.component.html',
  styleUrls: ['./kisi-listesi.component.scss'],
})
export class KisiListesiComponent implements OnInit {

  constructor(
    private kullaniciservice: KullaniciService,
    private router: Router) { }

  kisiListesi:Kisi[] = [];
  filteredKisiListesi: Kisi[] = [];
  filterText: string = "";
  veriYok: string = "";

  ngOnInit() {
    this.getKisiListesi();
  }

  getKisiListesi() {
    this.kullaniciservice.getKisiListesi().subscribe(data => {
      this.kisiListesi = data;
      this.filteredKisiListesi = data;
    }, error => {
      if (error.status == ResponseType.NotFound)
        this.veriYok = "Kişi yok.";
    });
  }

  filteredList() {
    this.kisiListesi = this.filteredKisiListesi.filter(item => {
      return (item.adSoyad).toLocaleLowerCase().indexOf(this.filterText.trim().toLocaleLowerCase()) > -1
    });
  }
}
