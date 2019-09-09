import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { PersonelListesiComponent } from './personel/personel-listesi/personel-listesi.component';
import { YeniPersonelComponent } from './personel/yeni-personel/yeni-personel.component';
import { YeniYoklamaComponent } from './yoklama/yeni-yoklama/yeni-yoklama.component';
import { YoklamaListesiComponent } from './yoklama/yoklama-listesi/yoklama-listesi.component';
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
    component: PersonelListesiComponent,
    canActivate: [PersonelGuard]
  },
  {
    path: 'ogrenci-yetki/:kisiId',
    loadChildren: './ogrenci/ogrenci-yetki/ogrenci-yetki.module#OgrenciYetkiModule',
    canActivate: [AdminGuard]
  },
  {
    path: 'yeni-personel',
    component: YeniPersonelComponent,
    canActivate: [PersonelGuard]
  },
  {
    path: 'personel-duzenle/:kisiId',
    component: YeniPersonelComponent,
    canActivate: [PersonelGuard]
  },
  {
    path: 'yeni-yoklama',
    component: YeniYoklamaComponent,
    canActivate: [YoklamaGuard]
  },
  {
    path: 'ogrenci-listesi',
    component: OgrenciListesiComponent,
    canActivate: [OgrenciGuard]
  },
  {
    path: 'yoklama-listesi',
    component: YoklamaListesiComponent,
    canActivate: [YoklamaGuard]
  },
  {
    path: 'yoklama-detay/:id',
    component: YeniYoklamaComponent,
    canActivate: [YoklamaGuard]
  },
  {
    path: 'hesap/giris',
    component: GirisComponent
  },
  {
    path: 'sifre-degistir',
    component: SifreDegistirComponent,
    canActivate: [LoginGuard]
  },
  {
    path: 'yoklama-baslik-listesi',
    component: YoklamaBaslikListesiComponent,
    canActivate: [YoklamaGuard]
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
