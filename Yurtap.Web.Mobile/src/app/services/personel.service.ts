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
        return this.httpService.getHttp<PersonelListe[]>(environment.personelPath, "getPersonelListesi");
    }
    addPersonel(personel: Personel) {
        return this.httpService.postHttp<Personel>(environment.personelPath, "addPersonel", personel);
    }
    deletePersonel(personel: Personel) {
        return this.httpService.deleteHttp<boolean>(environment.personelPath, "deletePersonel", personel);
    }
    getPersonelByKisiId(kisiId: number) {
        return this.httpService.getHttp<Personel>(environment.personelPath, "getPersonelByKisiId?kisiId=" + kisiId);
    }
    updatePersonel(personel: Personel) {
        return this.httpService.putHttp<Ogrenci>(environment.personelPath, "updatePersonel", personel);
    }
}