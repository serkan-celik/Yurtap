import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { Ogrenci } from '../models/Ogrenci';
import { PersonelListe } from '../models/PersonelListe';
import { Personel } from '../models/Personel';
import { environment } from 'src/environments/environment';
import { User } from '../models/account/User';
import { Rol } from '../models/account/CurrentUser';

@Injectable({
    providedIn: "root"
})
export class KullaniciService {
    constructor(private httpService: HttpService) {
    }
    updateKullanici(user: User) {
        return this.httpService.putHttp<User>(environment.kullaniciPath, "updateKullanici", user);
    }
    getKullaniciAdDogrula(kullaniciAd: string) {
        return this.httpService.getHttp<boolean>(environment.kullaniciPath, "getKullaniciAdDogrula?ad=" + kullaniciAd);
    }
    getKullaniciRolleriById(kisiId: number) {
        return this.httpService.getHttp<Rol[]>(environment.kullaniciPath, "getKullaniciRolleriById?kisiId=" + kisiId);
    }
    addKullaniciRol(rol: Rol) {
        return this.httpService.postHttp<Rol>(environment.kullaniciPath, "addKullaniciRol", rol);
    }
    deleteKullaniciRol(rol: Rol) {
        return this.httpService.deleteHttp<boolean>(environment.kullaniciPath, "deleteKullaniciRol", rol);
    }
    updateKullaniciRol(rol: Rol) {
        return this.httpService.putHttp<Rol>(environment.kullaniciPath, "updateKullaniciRol", rol);
    }
}