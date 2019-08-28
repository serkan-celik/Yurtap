import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { OgrenciListe } from '../models/OgrenciListe';
import { Ogrenci } from '../models/Ogrenci';
import { environment } from 'src/environments/environment';
import { DummyOgrenciListesi } from '../dummy/ogrenci';

@Injectable({
    providedIn: "root"
})
export class OgrenciService {
    constructor(private httpService: HttpService) {
    }
    getOgrenciListesi() {
        return this.httpService.getHttp<OgrenciListe[]>(environment.ogrenciPath, "getOgrenciListesi");
    }
    addOgrenci(ogrenci: Ogrenci) {
        return this.httpService.postHttp<Ogrenci>(environment.ogrenciPath, "addOgrenci", ogrenci);
    }
    deleteOgrenci(ogrenci: Ogrenci) {
        return this.httpService.deleteHttp<Ogrenci>(environment.ogrenciPath, "deleteOgrenci", ogrenci);
    }
    getOgrenciByKisiId(kisiId: number) {
        return this.httpService.getHttp<Ogrenci>(environment.ogrenciPath, "getOgrenciByKisiId?kisiId=" + kisiId);
    }
    updateOgrenci(ogrenci: Ogrenci) {
        return this.httpService.putHttp<Ogrenci>(environment.ogrenciPath, "updateOgrenci", ogrenci);
    }
}