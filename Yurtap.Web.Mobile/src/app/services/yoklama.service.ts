import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Yoklama } from '../models/Yoklama';
import { YoklamaListesi } from '../models/YoklamaListesi';
import { environment } from 'src/environments/environment';
import { DummyOgrenciListesi } from '../dummy/ogrenci';

@Injectable({
    providedIn: "root"
})
export class YoklamaService {
    constructor(private httpService: HttpService) {
    }

    getYoklamaListesi() {
        return this.httpService.getHttp<YoklamaListesi[]>(environment.yoklamaPath, "getYoklamaListesi");
    }

    addYoklama(yoklama: Yoklama) {
        return this.httpService.postHttp<Yoklama>(environment.yoklamaPath, "addYoklama", yoklama);
    }

    getYoklamaListeleriByTarih(tarih: string="") {
        return this.httpService.getHttp<Yoklama[]>(environment.yoklamaPath, "getYoklamaListeleri?tarih=" + tarih);
    }

    getYoklamaDetayById(id: number) {
        return this.httpService.getHttp<Yoklama>(environment.yoklamaPath, "getYoklamaDetayById?id=" + id);
    }

    updateYoklama(yoklama: Yoklama) {
        return this.httpService.putHttp<Yoklama>(environment.yoklamaPath, "updateYoklama", yoklama);
    }

    exportToExcelVakitlikYoklamaRaporu(yoklama: Yoklama) {
        return this.httpService.postHttp<any>(environment.yoklamaPath, "exportToExcelVakitlikYoklamaRaporu", yoklama, "blob");
    }

    exportToExcelAylikYoklamaKatilimDurumuRaporu(tarih: string, yoklamaBaslikId: number, yoklamaBaslik: String) {
        return this.httpService.getHttp<any>(environment.yoklamaPath, "exportToExcelAylikYoklamaKatilimDurumuRaporu?tarih=" + tarih + "&yoklamaBaslikId=" + yoklamaBaslikId + "&yoklamaBaslik=" + yoklamaBaslik, "blob");
    }

    exportToExcelAylikYoklamaKatilimYuzdesiRaporu(tarih: string) {
        return this.httpService.getHttp<any>(environment.yoklamaPath, "exportToExcelAylikYoklamaKatilimYuzdesiRaporu?tarih=" + tarih, "blob");
    }
}