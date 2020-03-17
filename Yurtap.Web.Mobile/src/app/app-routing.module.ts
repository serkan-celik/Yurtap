import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PersonelListesiComponent } from './personel/personel-listesi/personel-listesi.component';
import { YeniPersonelComponent } from './personel/yeni-personel/yeni-personel.component';
import { YeniYoklamaComponent } from './yoklama/yeni-yoklama/yeni-yoklama.component';
import { AdminGuard } from './hesap/guards/admin.guard'
import { GirisComponent } from './hesap/giris/giris.component'
import { SifreDegistirComponent } from './hesap/sifre-degistir/sifre-degistir.component';
import { YoklamaBaslikListesiComponent } from './yoklama/yoklama-baslik-listesi/yoklama-baslik-listesi.component';
import { PersonelGuard } from './hesap/guards/personel.guard';
import { YoklamaGuard } from './hesap/guards/yoklama.guard';
import { OgrenciGuard } from './hesap/guards/ogrenci.guard';
import { AnaSayfaComponent } from './ana-sayfa/ana-sayfa';
import { YeniOgrenciComponent } from './ogrenci/yeni-ogrenci/yeni-ogrenci.component';
import { LoginGuard } from './hesap/guards/login.guard';
import { OgrenciListesiComponent } from './ogrenci/ogrenci-listesi/ogrenci-listesi';
import { YoklamaRaporuComponent } from './rapor/yoklama-raporu/yoklama-raporu.component';
import { KullaniciListesiComponent } from './kullanici/kullanici-listesi/kullanici-listesi.component';
import { KisiListesiComponent } from './kullanici/kisi-listesi/kisi-listesi.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'ana-sayfa',
    pathMatch: 'full'
  },
  {
    path: 'ana-sayfa',
    loadChildren: './ana-sayfa/ana-sayfa.module#AnaSayfaModule',
  },
  {
    path: 'yeni-ogrenci',
    loadChildren: './ogrenci/yeni-ogrenci/yeni-ogrenci.module#YeniOgrenciModule',
    canActivate: [OgrenciGuard]
  },
  {
    path: 'ogrenci-duzenle/:kisiId',
    loadChildren: './ogrenci/yeni-ogrenci/yeni-ogrenci.module#YeniOgrenciModule',
    canActivate: [OgrenciGuard],
    /*children: [
      {
        path: 'ogrenci-yetki',
        children: [
          {
            path: '',
            loadChildren: './ogrenci/ogrenci-yetki/ogrenci-yetki.module#OgrenciYetkiModule',
          }
        ]
      }
    ]*/
  },
  {
    path: 'personel-listesi',
    loadChildren: './personel/personel-listesi/personel-listesi.module#PersonelListesiModule',
    canActivate: [PersonelGuard]
  },
  {
    path: 'yeni-yetki/:kisiId/:adSoyad',
    loadChildren: './kullanici/yeni-yetki/yeni-yetki.module#YeniYetkiModule',
    canActivate: [AdminGuard]
  },
  {
    path: 'yeni-personel',
    loadChildren: './personel/yeni-personel/yeni-personel.module#YeniPersonelModule',
    canActivate: [PersonelGuard]
  },
  {
    path: 'personel-duzenle/:kisiId',
    loadChildren: './personel/yeni-personel/yeni-personel.module#YeniPersonelModule',
    canActivate: [PersonelGuard]
  },
  {
    path: 'yeni-yoklama',
    loadChildren: './yoklama/yeni-yoklama/yeni-yoklama.module#YeniYoklamaModule',
    canActivate: [YoklamaGuard]
  },
  {
    path: 'ogrenci-listesi',
    loadChildren: './ogrenci/ogrenci-listesi/ogrenci-listesi.module#OgrenciListesiModule',
    canActivate: [OgrenciGuard]
  },
  {
    path: 'yoklama-listeleri',
    loadChildren: './yoklama/yoklama-listeleri/yoklama-listeleri.module#YoklamaListeleriModule',
    canActivate: [YoklamaGuard]
  },
  {
    path: 'yoklama-listesi/:id',
    loadChildren: './yoklama/yeni-yoklama/yeni-yoklama.module#YeniYoklamaModule',
    canActivate: [YoklamaGuard]
  },
  {
    path: 'hesap/giris',
    //loadChildren: './hesap/giris/giris.module#GirisModule',
    component: GirisComponent
  },
  {
    path: 'hesap/sifre-degistir',
    loadChildren: './hesap/sifre-degistir/sifre-degistir.module#SifreDegistirModule',
    canActivate: [LoginGuard]
  },
  {
    path: 'yoklama-baslik-listesi',
    loadChildren: './yoklama/yoklama-baslik-listesi/yoklama-baslik-listesi.module#YoklamaBaslikListesiModule',
    canActivate: [YoklamaGuard]
  },
  {
    path: 'yoklama-raporlari',
    //loadChildren: './rapor/yoklama-raporu/yoklama-raporu.module#YoklamaRaporuModule',
    component:YoklamaRaporuComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'kullanici-listesi',
    loadChildren: './kullanici/kullanici-listesi/kullanici-listesi.module#KullaniciListesiModule',
    canActivate: [AdminGuard]
  },
  {
    path: 'kisi-listesi',
   component:KisiListesiComponent,
    canActivate: [AdminGuard]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
