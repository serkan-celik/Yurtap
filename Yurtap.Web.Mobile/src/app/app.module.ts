import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ErrorsModule } from './errors';
import { YeniPersonelComponent } from './personel/yeni-personel/yeni-personel.component';
import { YapimAsamasindaComponent } from './errors/yapimAsamasinda/yapimAsamasinda.component';
import { SortPipe } from './pipes/SortPipe';
import { AdminGuard } from './hesap/guards/admin.guard'
import { GirisComponent } from './hesap/giris/giris.component'
import { SifreDegistirComponent } from './hesap/sifre-degistir/sifre-degistir.component';
import { PersonelGuard } from './hesap/guards/personel.guard';
import { YoklamaGuard } from './hesap/guards/yoklama.guard';
import { OgrenciGuard } from './hesap/guards/ogrenci.guard';
import { LoginGuard } from './hesap/guards/login.guard';
import { YoklamaRaporuComponent } from './rapor/yoklama-raporu/yoklama-raporu.component';
import { KullaniciListesiComponent } from './kullanici/kullanici-listesi/kullanici-listesi.component';
import { KisiListesiComponent } from './kullanici/kisi-listesi/kisi-listesi.component';
import { TextAvatarModule } from './components/text-avatar';
import { TextAvatarComponent } from './components/text-avatar/text-avatar.component';
import { PopModule } from './popcomponent/pop-module.module';

@NgModule({
  declarations: [
    YoklamaRaporuComponent,
    TextAvatarComponent,
    KisiListesiComponent,
    AppComponent,
    YapimAsamasindaComponent,
    SortPipe,
  GirisComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
   AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ErrorsModule,
    TextAvatarModule, PopModule,
  ],

  providers: [
    StatusBar,
    SplashScreen,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    AdminGuard,
    OgrenciGuard,
    PersonelGuard,
    YoklamaGuard,
    LoginGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule {

}
