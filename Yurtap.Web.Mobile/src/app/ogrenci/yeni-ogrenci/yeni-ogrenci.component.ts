import { Component, OnInit, ViewChild, ElementRef, SimpleChanges } from '@angular/core';
import { Ogrenci } from 'src/app/models/Ogrenci';
import { OgrenciService } from 'src/app/services/ogrenci.service';
import { ToastService } from 'src/app/services/toast/toast.service';
import { NavParams, IonApp } from '@ionic/angular';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { NgForm } from '@angular/forms';
import { ViewController } from '@ionic/core';
import { KimlikIslemleri } from 'src/app/utilities/KimlikIslemleri';
import { BaseComponent } from 'src/app/BaseComponent';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-yeni-ogrenci',
  templateUrl: './yeni-ogrenci.component.html',
  styleUrls: ['./yeni-ogrenci.component.scss'],
})
export class YeniOgrenciComponent extends BaseComponent implements OnInit {

  constructor(
    private ogrenciService: OgrenciService,
    private toastService: ToastService,
    private activatedRoute: ActivatedRoute, 
    public hesapService: HesapService,
    private alertService: AlertService) {
    super(hesapService);
  }

  ogrenci: Ogrenci = new Ogrenci();
  baslik: string = "Yeni Öğrenci";
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
      if (param.kisiId) {
        this.paramKisiId = param.kisiId;
        this.ogrenciService.getOgrenciByKisiId(param.kisiId).subscribe(data => {
          this.ogrenci = data.result;
          this.baslik = "Öğrenci Düzenle";
          this.duzenlemeModu = true;
          this.hesap = this.ogrenci.hesap;
        })
      }
    })
  }

  ogrenciKaydet(ogrenciForm: NgForm) {
    if (this.duzenlemeModu) {
      this.ogrenciService.updateOgrenci(this.ogrenci).subscribe(data => {
        if (data) {
          this.toastService.showToast("Öğrenci güncellendi");
        }
      })
    }
    else {
      this.ogrenciService.addOgrenci(this.ogrenci).subscribe(data => {
        if (data) {
          ogrenciForm.reset()
          this.toastService.showToast("Öğrenci oluşturuldu");
        }
      })
    }
    localStorage.setItem("ogrenci",JSON.stringify(this.ogrenci));
  }

  tcKimlikNoDogrula(tcKimlikNo) {
    this.hataMetni = new KimlikIslemleri().tcKimlikNoDogrula(tcKimlikNo);
  }
}
