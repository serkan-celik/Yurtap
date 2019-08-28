import { Component } from '@angular/core';

import { Platform, MenuController, IonSplitPane } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { HesapService } from './services/hesap/hesap.service';
import { BaseComponent } from './BaseComponent';
import { CurrentUser, Rol } from './models/account/CurrentUser';
import { RolEnum } from './enums/RolEnum';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent extends BaseComponent {
  public appPages = [
    {
      title: 'Ana Sayfa',
      url: '/ana-sayfa',
      icon: 'home',
      role:0
    },
    {
      title: 'Yeni Yoklama',
      url: '/yeni-yoklama',
      icon: 'checkbox-outline',
      role: this.getUserRoles.findIndex(p=>p.rolId==RolEnum.Admin)>-1 ? RolEnum.Admin : RolEnum.YoklamaYonetimi
    },
    {
      title: 'Yoklama Listesi',
      url: '/yoklama-listesi',
      icon: 'menu',
      role:this.getUserRoles.findIndex(p=>p.rolId==RolEnum.Admin)>-1 ? RolEnum.Admin : RolEnum.YoklamaYonetimi
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
      role:this.getUserRoles.findIndex(p=>p.rolId==RolEnum.Admin)>-1 ? RolEnum.Admin : RolEnum.OgrenciYonetimi
    },
    {
      title: 'Personel Listesi',
      url: '/personel-listesi',
      icon: 'person',
      role: this.getUserRoles.findIndex(p=>p.rolId==RolEnum.Admin)>-1 ? RolEnum.Admin :RolEnum.PersonelYonetimi
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

  roles:string

  ngOnInit() {
  

  }

  getAktifRol(roles){
    return this.getUserRoles.findIndex(p=>p.rolId==roles)>-1
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
