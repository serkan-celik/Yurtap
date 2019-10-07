import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Ogrenci } from '../models/Ogrenci';
import { PersonelListe } from '../models/PersonelListe';
import { Personel } from '../models/Personel';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: "root"
})
export class PersonelService {
    constructor(private httpService: HttpService) {
    }
    getPersonelListesi() {
        return this.httpService.getHttp<any>(environment.personelPath, "getPersonelListesi");
    }
    addPersonel(personel: Personel) {
        return this.httpService.postHttp<any>(environment.personelPath, "addPersonel", personel);
    }
    deletePersonel(personel: Personel) {
        return this.httpService.deleteHttp<any>(environment.personelPath, "deletePersonel", personel);
    }
    getPersonelByKisiId(kisiId: number) {
        return this.httpService.getHttp<any>(environment.personelPath, "getPersonelByKisiId?kisiId=" + kisiId);
    }
    updatePersonel(personel: Personel) {
        return this.httpService.putHttp<any>(environment.personelPath, "updatePersonel", personel);
    }
}