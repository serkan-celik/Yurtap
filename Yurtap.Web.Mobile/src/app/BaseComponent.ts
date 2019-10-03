import { HesapService } from './services/hesap/hesap.service';
import { CurrentUser, Rol } from './models/account/CurrentUser';
import { Injectable, OnInit } from '@angular/core';

@Injectable({
  providedIn: "root"
})
export class BaseComponent{

  public constructor(public hesapService: HesapService) {
  }
  user: CurrentUser = new CurrentUser();

  public get currentUser(): CurrentUser {
    if (this.hesapService.isLoggedIn)
      return this.hesapService.getCurrentUser();
    else
      return this.user;
  }

  public get getUserRoles(): Rol[] {
    return this.currentUser.roles.map((role: any) => {
      return {
        id: role.id,
        ad: role.Ad,
        rolId: role.RolId,
        kisiId: role.kisiId,
        ekleme: role.Ekleme,
        silme: role.Silme,
        guncelleme: role.Guncelleme,
        listeleme: role.Listeleme,
        arama: role.Arama
      }
    });
  }

}