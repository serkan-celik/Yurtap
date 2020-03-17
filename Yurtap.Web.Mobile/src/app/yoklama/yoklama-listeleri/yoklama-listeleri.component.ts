import { Component, OnInit } from '@angular/core';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { Yoklama } from 'src/app/models/Yoklama';
import * as moment from 'moment';
import { ResponseType } from 'src/app/consts/ResponseType';

@Component({
  selector: 'app-yoklama-listesi',
  templateUrl: './yoklama-listeleri.component.html',
  styleUrls: ['./yoklama-listeleri.component.scss'],
})
export class YoklamaListeleriComponent implements OnInit {

  constructor(private yoklamaService: YoklamaService) { }
  secilenTarih: string = moment().utc(true).format("YYYY-MM-DD")
  yoklamalarListesi: Yoklama[] = [];
  yoklamalarTakvimi: Yoklama[] = [];
  listeGorunum: boolean = true;
  takvimGorunum: boolean = false;
  veriYok: string = "";
  yoklamalarLength: number = 0;

  ngOnInit() {
    this.getYoklamalarListesi();
    this.getYoklamalarTakvimi();
  }

  ionViewWillEnter() {
    var length = localStorage.getItem("yoklamaLength");
    if (this.yoklamalarLength == parseInt(length)) {
      this.getYoklamalarListesi();
      localStorage.removeItem("yoklamaLength")
    }
  }

  getYoklamalarListesi() {
    this.yoklamaService.getYoklamaListeleriByTarih().subscribe(data => {
      this.yoklamalarListesi = data.result;
      this.yoklamalarLength = data.result.length;
    }, error => {
      if (error.status == ResponseType.NotFound) {
        this.veriYok = error.error.message;
      }
    });
  }

  getYoklamalarTakvimi() {
    this.yoklamaService.getYoklamaListeleriByTarih(this.secilenTarih.substring(0, 10)).subscribe(data => {
      this.yoklamalarTakvimi = data.result;
    }, error => {
      if (error.status == ResponseType.NotFound)
        this.yoklamalarTakvimi = [];
      this.veriYok = error.error.message;
    });
  }

  sonrakiTarih() {
    this.secilenTarih = moment(this.secilenTarih).add(1, 'days').toLocaleString()
  }

  oncekiTarih() {
    this.secilenTarih = moment(this.secilenTarih).subtract(1, 'days').toLocaleString();
  }

  clickListe() {
    this.listeGorunum = true;
    this.takvimGorunum = false;
  }

  clickTakvim() {
    this.takvimGorunum = true;
    this.listeGorunum = false;
  }
}
