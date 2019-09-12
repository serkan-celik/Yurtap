import { Component, OnInit } from '@angular/core';
import { YoklamaService } from 'src/app/services/yoklama.service';
import { Yoklama } from 'src/app/models/Yoklama';
import * as moment from 'moment';

@Component({
  selector: 'app-yoklama-listesi',
  templateUrl: './yoklama-listesi.component.html',
  styleUrls: ['./yoklama-listesi.component.scss'],
})
export class YoklamaListesiComponent implements OnInit {

  constructor(private yoklamaService: YoklamaService) { }
  secilenTarih: string = moment().utc(true).format("YYYY-MM-DD")
  yoklamalar :Yoklama[]=[];

  ngOnInit() {
   
  }

  ionViewWillEnter(){
    this.getYoklamaListesi();
  }

  getYoklamaListesi() {
    this.yoklamaService.getYoklamaListeleriByTarih(this.secilenTarih.substring(0,10)).subscribe(data => {
      this.yoklamalar = data;
    });
  }

  sonrakiTarih(){
    this.secilenTarih =  moment(this.secilenTarih).add(1,'days').toLocaleString()
  }

  oncekiTarih(){
    this.secilenTarih =  moment(this.secilenTarih).subtract(1,'days').toLocaleString();
  }
}
