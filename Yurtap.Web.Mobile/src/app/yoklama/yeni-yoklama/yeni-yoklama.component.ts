import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { YoklamaListesi } from 'src/app/models/YoklamaListesi';
import { Yoklama } from '../../models/Yoklama';
import { AlertService } from 'src/app/services/alert/alert.service';
import { YoklamaDurumEnum } from 'src/app/enums/YoklamaDurumEnum';
import { ActivatedRoute } from '@angular/router';
import { YoklamaBaslikService } from 'src/app/services/yoklama-baslik.service';
import { YoklamaBaslik } from 'src/app/models/YoklamaBaslik';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { saveAs } from 'file-saver';
import { Observable } from 'rxjs';
import * as moment from 'moment';
import { NgForm } from '@angular/forms';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-yeni-yoklama',
  templateUrl: './yeni-yoklama.component.html',
  styleUrls: ['./yeni-yoklama.component.scss'],
})
export class YeniYoklamaComponent implements OnInit {

  constructor(
    private yoklamaService: YoklamaService,
    private yoklamaBaslikService: YoklamaBaslikService,
    private alertService: AlertService,
    private activatedRoute: ActivatedRoute,
    private toastService: ToastService) { }

  filteredOgrenciListesi: YoklamaListesi[] = [];
  yoklamaBaslikListesi: YoklamaBaslik[] = [];
  filterText: string = "";
  yoklama: Yoklama = new Yoklama();
  baslik: string;
  duzenlemeModu: boolean;
  veriYok: string = "";
  yoklamaDurum: number = -1;

  yoklamaTurleri = [
    { "text": "Yok", "value": 0 },
    { "text": "Var", "value": 1 },
    { "text": "Okulda", "value": 2 },
    { "text": "İzinli", "value": 3 },
    { "text": "Görevli", "value": 4 },
    { "text": "Hasta", "value": 5 }
  ]

  ngOnInit() {

  }

  ionViewDidEnter() {
    this.getYoklama();
    this.getYoklamaBaslikListesi();
  }

  getYoklamaListesi() {
    this.yoklamaService.getYoklamaListesi().subscribe(data => {
      if (data.length == 0) {
        this.veriYok = "Öğrenci kaydı olmadan yoklama alınamaz";
        return;
      }
      this.yoklama.yoklamaListesi = data;
      this.filteredOgrenciListesi = data;
    });
  }

  getYoklamaBaslikListesi() {
    this.yoklamaBaslikService.getYoklamaBaslikListesi().subscribe(data => {
      this.yoklamaBaslikListesi = data;
    });
  }

  getYoklama() {
    this.activatedRoute.params.subscribe(param => {
      if (!param.id) {
        this.getYoklamaListesi();
        this.baslik = "Yeni Yoklama";
        this.duzenlemeModu = false;
      }
      else {
        this.yoklamaService.getYoklamaDetayById(param.id).subscribe(data => {
          this.yoklama = data;
          this.filteredOgrenciListesi = data.yoklamaListesi;
          this.baslik = "Yoklama Düzenle";
          this.duzenlemeModu = true;
        });
      }
    });
    /*var param = this.activatedRoute.snapshot.paramMap.get("id")
     if (!param) {
       this.getYoklamaListesi();
       this.baslik = "Yeni Yoklama";
       this.duzenlemeModu = false;
     return;
     }
     else{
       this.yoklamaService.getYoklamaById(parseInt(param)).subscribe(data => {
         this.yoklama = data;
         this.filteredOgrenciListesi = data.yoklamaListesi;
         this.baslik = "Yoklama Düzenle";
       });
     }
     this.duzenlemeModu = true;*/
  }

  filteredList() {
    this.yoklama.yoklamaListesi = this.filteredOgrenciListesi.filter(item => {
      let result = (item.ad + " " + item.soyad).toLocaleLowerCase().indexOf(this.filterText.trim().toLocaleLowerCase()) > -1
      if (result)
        return result
    });

  }

  yoklamaKaydet(yoklamaForm: NgForm) {
    if (yoklamaForm.invalid) {
      this.toastService.showToast("Lütfen yoklama başlığı seçiniz")
      return;
    }
    this.yoklama.tarih = moment(this.yoklama.tarih).format("YYYY-MM-DD HH:mm")
    if (!this.duzenlemeModu) {
      this.alertService.confirmAlert("Yoklama kaydedilsin mi?", this.yoklamaService.addYoklama(this.yoklama), "Yoklama başarıyla kaydedildi");

    }
    else {
      this.alertService.confirmAlert("Yoklama güncellensin mi?", this.yoklamaService.updateYoklama(this.yoklama), "Yoklama başarıyla güncellendi")
    }
  }

  changeItemColor(item, yoklamaListesi: YoklamaListesi) {
    switch (yoklamaListesi.yoklamaDurum) {
      case YoklamaDurumEnum.Var:
        item.color = "success";
        yoklamaListesi.durum = "Var";
        break;
      case YoklamaDurumEnum.Yok:
        item.color = "danger";
        yoklamaListesi.durum = "Yok";
        break;
      case YoklamaDurumEnum.Okulda:
        item.color = "secondary";
        yoklamaListesi.durum = "Okulda";
        break;
      case YoklamaDurumEnum.Izınli:
        item.color = "warning";
        yoklamaListesi.durum = "İzinli";
        break;
      case YoklamaDurumEnum.Gorevli:
        item.color = "primary";
        yoklamaListesi.durum = "Görevli";
        break;
      case YoklamaDurumEnum.Rahatsız:
        item.color = "light";
        yoklamaListesi.durum = "Rahatsız";
        break;
    }
  }

  exportToExcel() {
    this.toastService.showToast("İndiriliyor...");
    this.yoklamaService.exportToExcelVakitlikYoklamaRaporu(this.yoklama)
      .subscribe(
        data => {
          saveAs(data, moment(this.yoklama.tarih).format("DD.MM.YYYY HH.mm") + "-" + this.yoklama.baslik + '.xlsx');
        },
        error => {
          console.log("Something went wrong");
        }
      );
  }

  changeYoklamaDurum() {
    if (this.yoklamaDurum == -1) {
      this.yoklama.yoklamaListesi = this.filteredOgrenciListesi;
      return;
    }
    this.yoklama.yoklamaListesi = this.filteredOgrenciListesi.filter(item => {
      let result = item.yoklamaDurum == this.yoklamaDurum;
      if (result)
        return result
      else
        this.veriYok = "Aranılan kriterde yoklama yoktur";
    });
  }
}