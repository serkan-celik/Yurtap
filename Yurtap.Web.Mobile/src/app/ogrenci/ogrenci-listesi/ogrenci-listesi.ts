import { Component, OnInit } from '@angular/core';
import { OgrenciListe } from '../../models/OgrenciListe';
import { HttpService } from '../../services/http.service';
import { OgrenciService } from '../../services/ogrenci.service';
import { Router } from '@angular/router';
import { AlertService } from '../../services/alert/alert.service';
import { Ogrenci } from '../../models/Ogrenci';
import { SortPipe } from 'src/app/pipes/SortPipe';
import { BaseComponent } from 'src/app/BaseComponent';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { RolEnum } from 'src/app/enums/RolEnum';
import { Rol } from 'src/app/models/account/CurrentUser';

@Component({
  selector: 'app-list',
  templateUrl: 'ogrenci-listesi.html',
  styleUrls: ['ogrenci-listesi.scss'],
})
export class OgrenciListesiComponent implements OnInit {
  private selectedItem: any;
  private icons = [
    'flask',
    'wifi',
    'beer',
    'football',
    'basketball',
    'paper-plane',
    'american-football',
    'boat',
    'bluetooth',
    'build'
  ];
  public items: Array<{ title: string; note: string; icon: string }> = [];
  constructor(
    private ogrenciService: OgrenciService,
    private router: Router, private alertService: AlertService,public hesapService:HesapService) {
    //super(hesapService);
    for (let i = 1; i < 11; i++) {
      this.items.push({
        title: 'Item ' + i,
        note: 'This is item #' + i,
        icon: this.icons[Math.floor(Math.random() * this.icons.length)]
      });
    }
  }

  ogrenciListesi: OgrenciListe[] = [];
  filteredOgrenciListesi: OgrenciListe[] = [];
  filterText: string = "";
  veriYok:string="";

  rol :Rol;

  ngOnInit() {
 
  }

  ionViewDidEnter(){
    this.getOgrenciListesi();
  }

  getOgrenciListesi() {
    this.ogrenciService.getOgrenciListesi().subscribe(data => {
      if(data.length == 0){
        this.veriYok = "Kayıtlı öğrenci yok. Eklemek için butona tıklayınız.";
        return;
      }
      this.ogrenciListesi = data; 
      this.filteredOgrenciListesi = data;    
    });
  }

  //Server-side filtering
  /* filteredList() {
     this.ogrenciService.getOgrenciListesi().subscribe(
       data => this.ogrenciListesi = data.filter(item => {
       return (item.ad + " " + item.soyad).toLocaleLowerCase().indexOf(this.filterText.toLocaleLowerCase()) > -1
     }));
  }*/

    //Client-side filtering
     filteredList() {
      this.ogrenciListesi = this.filteredOgrenciListesi.filter(item => {
       return (item.ad + " " + item.soyad).toLocaleLowerCase().indexOf(this.filterText.trim().toLocaleLowerCase()) > -1
     });
  }

  // add back when alpha.4 is out
  // navigate(item) {
  //   this.router.navigate(['/list', JSON.stringify(item)]);
  // }
  ogrenciEkle() {
    this.router.navigateByUrl("yeni-ogrenci")
  }
  ogrenciSil(ogrenci) {
    this.alertService.confirmDeleteAlert(ogrenci.ad + " " + ogrenci.soyad + " silinsin mi?", this.ogrenciService.deleteOgrenci(ogrenci), this.ogrenciListesi, ogrenci, "Kayıt silindi")
  }
  ogrenciDuzenle(ogrenci) {
    this.router.navigateByUrl("/ogrenci-duzenle/" + ogrenci.kisiId)
  }
}
