import { Component, OnInit } from '@angular/core';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { ActivatedRoute } from '@angular/router';
import { Yoklama } from 'src/app/models/Yoklama';
import { YoklamaDurumEnum } from 'src/app/enums/YoklamaDurumEnum';
import { YoklamaListesi } from 'src/app/models/YoklamaListesi';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-yoklama-detay',
  templateUrl: './yoklama-detay.component.html',
  styleUrls: ['./yoklama-detay.component.scss'],
})
export class YoklamaListesiComponent implements OnInit {

  constructor(
    private yoklamaService: YoklamaService,
    private activatedRoute: ActivatedRoute,
    private alertService: AlertService) { }

  yoklama: Yoklama = new Yoklama();
  filteredOgrenciListesi: YoklamaListesi[] = [];
  filterText: string = "";

  ngOnInit() {
    this.getYoklama();
  }

  getYoklama() {
    this.activatedRoute.params.subscribe(param => {
      if (param.id)
        this.yoklamaService.getYoklamaDetayById(param.id).subscribe(data => {
          this.yoklama = data;
          this.filteredOgrenciListesi = data.yoklamaListesi;
        });
    });
  }

  filteredList() {
    this.yoklama.yoklamaListesi = this.filteredOgrenciListesi.filter(item => {
      let result = (item.ad + " " + item.soyad).toLocaleLowerCase().indexOf(this.filterText.trim().toLocaleLowerCase()) > -1
      if(result)
      return result
    });

  }

  changeItemColor(item, yoklamaListesi: YoklamaListesi) {
    switch (yoklamaListesi.yoklamaDurum) {
      case YoklamaDurumEnum.Var:
        item.color = "success";
        break;
      case YoklamaDurumEnum.Yok:
        item.color = "danger";
        break;
      case YoklamaDurumEnum.Izınli:
        item.color = "warning";
        break;
      case YoklamaDurumEnum.Gorevli:
        item.color = "primary";
        break;
      case YoklamaDurumEnum.Rahatsız:
        item.color = "light";
        break;
    }
  }

  yoklamaKaydet() {
    this.alertService.confirmAlert("Yoklama güncellensin mi?", this.yoklamaService.updateYoklama(this.yoklama), "Yoklama başarıyla güncellendi")
  }
}
