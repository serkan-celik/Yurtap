import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastService } from 'src/app/services/toast/toast.service';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Personel } from 'src/app/models/Personel';
import { NgForm } from '@angular/forms';
import { PersonelService } from 'src/app/services/personel.service';
import { KimlikIslemleri } from 'src/app/utilities/KimlikIslemleri';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-yeni-personel',
  templateUrl: './yeni-personel.component.html',
  styleUrls: ['./yeni-personel.component.scss'],
})
export class YeniPersonelComponent implements OnInit {

  constructor(
    private personelService: PersonelService,
    private toastService: ToastService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private alertService: AlertService) { }

  personel: Personel = new Personel();
  baslik: string = "Yeni Personel";
  duzenlemeModu: boolean = false;
  @ViewChild('ad') ad;
  hataMetni = "";
  paramKisiId: number;
  hesap:boolean;

  public ngAfterContentInit() {
    this.setFocus();
  }

  setFocus() {
    setTimeout(() => {
      this.ad.setFocus();
    }, 0);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(param => {
      if (param.kisiId){
        this.paramKisiId = param.kisiId;
        this.personelService.getPersonelByKisiId(param.kisiId).subscribe(data => {
          this.personel = data;
          this.baslik = "Personel Düzenle";
          this.duzenlemeModu = true;
          this.hesap = this.personel.hesap;
        })
      }
    })
  }

  personelKaydet(personelForm: NgForm) {
    if (this.duzenlemeModu) {
      this.personelService.updatePersonel(this.personel).subscribe(data => {
        if (data) {
          this.toastService.showToast("Personel güncellendi.");
        }
      }, error => {
        console.log(error.error);
        this.toastService.showToast(error.error);
      })
    }
    else {
      this.personelService.addPersonel(this.personel).subscribe(data => {
        if (data) {
          personelForm.reset()
          this.toastService.showToast("Personel kaydedildi.");
        }
      });
    }
  }

  tcKimlikNoDogrula(tcKimlikNo) {
    this.hataMetni = new KimlikIslemleri().tcKimlikNoDogrula(tcKimlikNo);
  }

  hesapGoster(){
    this.alertService.advancedAlert("Kullanıcı Hesabı","Kullanıcı Adı: " + this.personel.kullaniciAd + "<br/>Şifre: " + this.personel.sifre);
  }
}
