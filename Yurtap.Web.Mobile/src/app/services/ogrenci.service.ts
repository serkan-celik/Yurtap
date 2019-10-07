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
        return this.httpService.getHttp<any>(environment.ogrenciPath, "getOgrenciListesi");
    }
    addOgrenci(ogrenci: Ogrenci) {
        return this.httpService.postHttp<any>(environment.ogrenciPath, "addOgrenci", ogrenci);
    }
    deleteOgrenci(ogrenci: Ogrenci) {
        return this.httpService.deleteHttp<any>(environment.ogrenciPath, "deleteOgrenci", ogrenci);
    }
    getOgrenciByKisiId(kisiId: number) {
        return this.httpService.getHttp<any>(environment.ogrenciPath, "getOgrenciByKisiId?kisiId=" + kisiId);
    }
    updateOgrenci(ogrenci: Ogrenci) {
        return this.httpService.putHttp<any>(environment.ogrenciPath, "updateOgrenci", ogrenci);
    }
}