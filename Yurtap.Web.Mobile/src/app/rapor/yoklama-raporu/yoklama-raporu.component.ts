import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { YoklamaBaslikService } from 'src/app/services/yoklama-baslik.service';
import { YoklamaBaslik } from 'src/app/models/YoklamaBaslik';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { Yoklama } from 'src/app/models/Yoklama';
import { ToastService } from 'src/app/services/toast/toast.service';
import { saveAs } from 'file-saver';
import { AlertService } from 'src/app/services/alert/alert.service';

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
    private alertService: AlertService
  ) { }
  katilim: string = "durumluk";
  rapor: string = "aylık";
  tarih: string = moment().toLocaleString();
  yoklamaBaslikListesi: YoklamaBaslik[] = [];
  yoklamaBaslikId: number;
  yoklamalar: Yoklama[] = [];
  yoklama: Yoklama = new Yoklama();
  yoklamaBaslik: YoklamaBaslik = new YoklamaBaslik;

  ngOnInit() {
    this.getYoklamaBaslikListesi();
  }

  getYoklamaBaslikListesi() {
    this.yoklamaBaslikService.getYoklamaBaslikListesi().subscribe(data => {
      this.yoklamaBaslikListesi = data;
    })
  }

  getYoklamaListesi() {
    this.yoklamaService.getYoklamaListeleriByTarih(this.tarih.substring(0, 10)).subscribe(data => {
      this.yoklamalar = data;
    });
  }

  raporla() {
    if (this.katilim == 'durumluk' && this.rapor == 'saatlik') {
      this.yoklamaService.exportToExcelVakitlikYoklamaRaporu(this.yoklama)
        .subscribe(
          data => {
            this.toastService.showToast("İndiriliyor...");
            saveAs(data, moment(this.yoklama.tarih).format("DD.MM.YYYY HH.mm") + "-" + this.yoklama.baslik + '.xlsx');
          },
          error => {
            this.alertService.basicAlert("Seçilen kriterlerde rapor yoktur.");
          }
        );
    } else if (this.katilim == 'durumluk' && this.rapor == 'aylık') {
      this.yoklamaService.exportToExcelAylikYoklamaKatilimDurumuRaporu(this.tarih.substring(0, 10), this.yoklamaBaslik.id, this.yoklamaBaslik.baslik)
        .subscribe(
          data => {
            this.toastService.showToast("İndiriliyor...");
            saveAs(data, moment(this.tarih).format("MM.YYYY") + "-" + this.yoklamaBaslik.baslik + '.xlsx');
          },
          error => {
            this.alertService.basicAlert("Seçilen kriterlerde rapor yoktur.");
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
            this.alertService.basicAlert("Seçilen kriterlerde rapor yoktur.");
          }
        );
    }
  }
}
