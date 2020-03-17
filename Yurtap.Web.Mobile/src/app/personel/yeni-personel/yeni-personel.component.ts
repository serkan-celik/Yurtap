import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastService } from 'src/app/services/toast/toast.service';
import { ActivatedRoute} from '@angular/router';
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
          this.personel = data.result;
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
        if (data.result) {
          this.toastService.showToast("Personel güncellendi");
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
          this.toastService.showToast("Personel oluşturuldu");
        }
      });
    }
    localStorage.setItem("personel",JSON.stringify(this.personel));
  }

  tcKimlikNoDogrula(tcKimlikNo) {
    this.hataMetni = new KimlikIslemleri().tcKimlikNoDogrula(tcKimlikNo);
  }
}
