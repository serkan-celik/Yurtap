import { Component, OnInit, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import * as moment from 'moment';
import { YoklamaBaslikService } from 'src/app/services/yoklama-baslik.service';
import { YoklamaBaslik } from 'src/app/models/YoklamaBaslik';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { Yoklama } from 'src/app/models/Yoklama';
import { ToastService } from 'src/app/services/toast/toast.service';
import { saveAs } from 'file-saver';
import { AlertService } from 'src/app/services/alert/alert.service';
import { ResponseType } from 'src/app/consts/ResponseType';

@Component({
  selector: 'app-yoklama-raporu',
  templateUrl: './yoklama-raporu.component.html',
  styleUrls: ['./yoklama-raporu.component.scss'],
})
export class YoklamaRaporuComponent implements OnInit {

  constructor(
    private yoklamaBaslikService: YoklamaBaslikService,
    private yoklamaService: YoklamaService,
    private toastService: ToastService,
    private alertService: AlertService,
    public cdr: ChangeDetectorRef,
  ) { }
  katilim: string = "durumluk";
  rapor: string = "aylık";
  tarih: string = moment().toLocaleString();
  yoklamaBaslikListesi: YoklamaBaslik[] = [];
  yoklamalarListesi: Yoklama[] = [];
  yoklamaListesi: Yoklama = new Yoklama();
  yoklamaBaslik: YoklamaBaslik = new YoklamaBaslik;


  ngOnInit() {
    this.getYoklamaBaslikListesi();
    this.getYoklamaListesi();
  }

  getYoklamaBaslikListesi() {
    this.yoklamaBaslikService.getYoklamaBaslikListesi().subscribe(data => {
      this.yoklamaBaslikListesi = data.result;
    },
      error => {
        if (error.status == ResponseType.NotFound)
          this.toastService.showToast(error.error.message);
      })
  }

  getYoklamaListesi() {
    this.yoklamaService.getYoklamaListeleriByTarih(this.tarih.substring(0, 10)).subscribe(data => {
      this.yoklamalarListesi = data.result;
    },
      error => {
        if (error.status == ResponseType.NotFound) {
           this.yoklamalarListesi = [];
          this.yoklamaListesi = new Yoklama();
          //this.toastService.showToast(error.error.message);
        }
      });
  }

  raporla() {
    if (this.katilim == 'durumluk' && this.rapor == 'saatlik') {
      if (this.yoklamaListesi.id == undefined&&this.yoklamalarListesi.length>0) {
        this.toastService.showToast("Lütfen yoklama seçiniz");
        return;
      }
      this.yoklamaService.exportToExcelVakitlikYoklamaRaporu(this.yoklamaListesi)
        .subscribe(
          data => {
            this.toastService.showToast("İndiriliyor...");
            saveAs(data, moment(this.yoklamaListesi.tarih).format("DD.MM.YYYY HH.mm") + "-" + this.yoklamaListesi.baslik + '.xlsx');
          },
          error => {
            if (error.status == ResponseType.NotFound || error.status == ResponseType.BadRequest)
              this.alertService.basicAlert("Seçilen kriterlerde rapor bulunamadı.");
          }
        );
    } else if (this.katilim == 'durumluk' && this.rapor == 'aylık') {
      if (this.yoklamaBaslik.id == undefined) {
        this.toastService.showToast("Lütfen yoklama başlığı seçiniz");
        return;
      }
      this.yoklamaService.exportToExcelAylikYoklamaKatilimDurumuRaporu(this.tarih.substring(0, 10), this.yoklamaBaslik.id, this.yoklamaBaslik.baslik)
        .subscribe(
          data => {
            this.toastService.showToast("İndiriliyor...");
            saveAs(data, moment(this.tarih).format("MM.YYYY") + "-" + this.yoklamaBaslik.baslik + '.xlsx');
          },
          error => {
            if (error.status == ResponseType.NotFound)
              this.alertService.basicAlert("Seçilen kriterlerde rapor bulunamadı.");
          }
        );
    }
    else if (this.katilim == 'yüzdelik' && this.rapor == 'aylık') {
      this.yoklamaService.exportToExcelAylikYoklamaKatilimYuzdesiRaporu(this.tarih.substring(0, 10))
        .subscribe(
          data => {
            this.toastService.showToast("İndiriliyor...");
            saveAs(data, moment(this.tarih).format("MM.YYYY") + "-Aylık Yüzdelik Katılım.xlsx");
          },
          error => {
            if (error.status == ResponseType.NotFound)
              this.alertService.basicAlert("Seçilen kriterlerde rapor bulunamadı.");
          }
        );
    }
  }
}
