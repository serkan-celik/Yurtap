import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { AlertService } from 'src/app/services/alert/alert.service';
import { PersonelListe } from 'src/app/models/PersonelListe';
import { PersonelService } from 'src/app/services/personel.service';

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

    personelListesi: PersonelListe[]=[];
    filteredPersonelListesi: PersonelListe[]=[];
    filterText: string = "";
    veriYok:string="";
    
    ngOnInit() {
    
    }

    ionViewWillEnter(){
        this.getPersonelListesi();
    }

    public getPersonelListesi() {
      this.personelService.getPersonelListesi().subscribe(data => {
        if(data.length == 0){
          this.veriYok = "Kayıtlı personel yok. Eklemek için butona tıklayınız.";
          return;
        }  
        this.personelListesi = data; 
        this.filteredPersonelListesi = data;
      });
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
    this.alertService.confirmDeleteAlert(personel.ad + " " + personel.soyad + " silinsin mi?", this.personelService.deletePersonel(personel), this.personelListesi, personel,"Personel başarıyla silindi");
  }
  personelDuzenle(personel) {
    this.router.navigateByUrl("/personel-duzenle/" + personel.kisiId)
  }
}
