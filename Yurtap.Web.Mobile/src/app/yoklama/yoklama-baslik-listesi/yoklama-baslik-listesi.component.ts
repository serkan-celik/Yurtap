import { Component, OnInit } from '@angular/core';
import { YoklamaBaslikService } from 'src/app/services/yoklama-baslik.service';
import { YoklamaBaslik } from 'src/app/models/YoklamaBaslik';
import { AlertService } from 'src/app/services/alert/alert.service';
import { ToastService } from 'src/app/services/toast/toast.service';

@Component({
  selector: 'app-yoklama-baslik-listesi',
  templateUrl: './yoklama-baslik-listesi.component.html',
  styleUrls: ['./yoklama-baslik-listesi.component.scss'],
})
export class YoklamaBaslikListesiComponent implements OnInit {

  constructor(
    private yoklamaBaslikService: YoklamaBaslikService,
    private alertService: AlertService,
    private toastService: ToastService) { }
  yoklamaBaslikListesi: YoklamaBaslik[] = [];
  yoklamaBaslik: YoklamaBaslik = new YoklamaBaslik();

  ngOnInit() {

  }

  ionViewDidEnter() {
    this.getYoklamaBaslikListesi();
  }

  getYoklamaBaslikListesi() {
    this.yoklamaBaslikService.getYoklamaBaslikListesi().subscribe(data => {
      this.yoklamaBaslikListesi = data;
    })
  }

  yoklamaBaslikDuzenle(yoklamaBaslik) {
    this.alertService.inputAlert(yoklamaBaslik.id > 0 ? "Başlık Düzenle" : "Başlık Ekle",
      [
        {
          name: 'baslik',
          type: 'text',
          value: yoklamaBaslik.baslik
        }
      ],
      [
        {
          text: "Kaydet",
          handler: (data) => {
            if (data['baslik'].length == 0) {
              const firstInput: any = document.querySelector('ion-alert input');
              firstInput.focus();
              firstInput.required;
              return false;
            }
            yoklamaBaslik.baslik = data['baslik'];
            if (yoklamaBaslik.id > 0) {
              this.yoklamaBaslikService.updateYoklamaBaslik(yoklamaBaslik).subscribe(data => {
                if (data)
                  this.getYoklamaBaslikListesi();
                this.toastService.showToast("Yoklama başlığı güncellendi")
              })
            }
            else {
              this.yoklamaBaslikService.addYoklamaBaslik(yoklamaBaslik).subscribe(data => {
                if (data)
                  this.getYoklamaBaslikListesi();
                this.toastService.showToast("Yoklama başlığı eklendi");
              })
            }
          }
        },
        {
          text: "Vazgeç",
        }
      ]).then(() => {
        const firstInput: any = document.querySelector('ion-alert input');
        firstInput.focus();
        return;
      })
  }

  yoklamaBaslikSil(yoklamaBaslik) {
    this.alertService.confirmDeleteAlert(yoklamaBaslik.baslik + " başlığı silinsin mi?", this.yoklamaBaslikService.deleteYoklamaBaslik(yoklamaBaslik), this.yoklamaBaslikListesi, yoklamaBaslik, "Yoklama başlığı silindi");
  }
}
