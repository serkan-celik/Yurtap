import { Component } from '@angular/core';

import { Platform, MenuController, IonSplitPane } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { HesapService } from './services/hesap/hesap.service';
import { BaseComponent } from './BaseComponent';
import { CurrentUser, Rol } from './models/account/CurrentUser';
import { RolEnum } from './enums/RolEnum';
import { Router, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent extends BaseComponent {
  public appPages = [
    {
      title: 'Yeni Yoklama',
      url: '/yeni-yoklama',
      icon: 'checkbox-outline',
      role: [
        RolEnum.YoklamaYonetimi.valueOf()
      ]
    },
    {
      title: 'Yoklama Listesi',
      url: '/yoklama-listesi',
      icon: 'menu',
      role:[
        RolEnum.YoklamaYonetimi.valueOf()
      ]
    },
    /*{
      title: 'Yoklama Başlık Listesi',
      url: '/yoklama-baslik-listesi',
      icon: 'menu',
      role:this.getUserRoles.findIndex(p=>p.rolId==RolEnum.Admin)>-1 ? RolEnum.Admin :RolEnum.YoklamaYonetimi,
    },*/
    {
      title: 'Öğrenci Listesi',
      url: '/ogrenci-listesi',
      icon: 'contacts',
      role:[
        RolEnum.OgrenciYonetimi.valueOf()
      ]
    },
    {
      title: 'Personel Listesi',
      url: '/personel-listesi',
      icon: 'person',
      role:[
        RolEnum.PersonelYonetimi.valueOf()
      ]
    }
  ];

  constructor(
    public platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private menuCtrl: MenuController,
    public hesapService: HesapService,
  ) {
    super(hesapService);
    this.initializeApp();
}

  ngOnInit() {

  }

  getAktifRol(role:number){
    return this.getUserRoles.findIndex(p=>p.rolId==role)>-1;
  }

  roles(){
    return this.getUserRoles;
  }

  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }

  logOut() {
    this.hesapService.logOut();
    this.menuCtrl.close();
  }

  menuDurum: boolean;
  menuyuKapat() {
    this.menuDurum == false ? this.menuDurum = true : this.menuDurum = false;
  }
}
