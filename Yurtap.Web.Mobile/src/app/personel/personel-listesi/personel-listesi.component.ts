import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/services/alert/alert.service';
import { PersonelListe } from 'src/app/models/PersonelListe';
import { PersonelService } from 'src/app/services/personel.service';
import { IonItemSliding } from '@ionic/angular';
import { ResponseType } from 'src/app/consts/ResponseType';

@Component({
  selector: 'app-personel-listesi',
  templateUrl: './personel-listesi.component.html',
  styleUrls: ['./personel-listesi.component.scss'],
})
export class PersonelListesiComponent implements OnInit {

  constructor(
    private personelService: PersonelService,
    private router: Router,
    private alertService: AlertService) { }

  personelListesi: PersonelListe[] = [];
  filteredPersonelListesi: PersonelListe[] = [];
  filterText: string = "";
  veriYok: string = "";

  ngOnInit() {
    this.getPersonelListesi();
  }

  ionViewWillEnter() {
    var personel = JSON.parse(localStorage.getItem("personel"));
    if (personel && (JSON.stringify(personel) != JSON.stringify(this.personelListesi.find(o => o.kisiId == personel.kisiId)))) {
      this.getPersonelListesi();
    }
    if (personel)
      localStorage.removeItem("personel");
  }

  public getPersonelListesi() {
    this.personelService.getPersonelListesi().subscribe(data => {
      this.personelListesi = data.result;
      this.filteredPersonelListesi = data.result;
    }, error => {
      if (error.status == ResponseType.NotFound)
        this.veriYok = "Kayıtlı personel yok. Eklemek için butona tıklayınız."
    })
  }

  filteredList() {
    this.personelListesi = this.filteredPersonelListesi.filter(item => {
      return (item.ad + " " + item.soyad).toLocaleLowerCase().indexOf(this.filterText.toLocaleLowerCase()) > -1
    });
  }

  personelEkle() {
    this.router.navigateByUrl("yeni-personel")
  }
  personelSil(personel) {
    this.alertService.confirmDeleteAlert("<b>" + personel.ad + " " + personel.soyad + "</b> silinsin mi?", this.personelService.deletePersonel(personel), this.personelListesi, personel, "Personel başarıyla silindi");
  }
  personelDuzenle(personel, itemSliding: IonItemSliding) {
    localStorage.setItem("personel", JSON.stringify(personel));
    this.router.navigateByUrl("/personel-duzenle/" + personel.kisiId);
    itemSliding.close();
  }
}
