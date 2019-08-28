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
import { PersonelListesiComponent } from './personel/personel-listesi/personel-listesi.component';
import { YeniPersonelComponent } from './personel/yeni-personel/yeni-personel.component';
import { YeniYoklamaComponent } from './yoklama/yeni-yoklama/yeni-yoklama.component';
import { YoklamaListesiComponent } from './yoklama/yoklama-listesi/yoklama-listesi.component';
import { YoklamaDetayComponent } from './yoklama/yoklama-detay/yoklama-detay.component';
import { YapimAsamasindaComponent } from './errors/yapimAsamasinda/yapimAsamasinda.component';
import { SortPipe } from './pipes/SortPipe';
import { AdminGuard } from './hesap/guards/admin.guard'
import { GirisComponent } from './hesap/giris/giris.component'
import { SifreDegistirComponent } from './hesap/sifre-degistir/sifre-degistir.component';
import { YoklamaBaslikListesiComponent } from './yoklama/yoklama-baslik-listesi/yoklama-baslik-listesi.component';
import { PersonelGuard } from './hesap/guards/personel.guard';
import { YoklamaGuard } from './hesap/guards/yoklama.guard';
import { OgrenciGuard } from './hesap/guards/ogrenci.guard';
import { YeniOgrenciComponent } from './ogrenci/yeni-ogrenci/yeni-ogrenci.component';
import { LoginGuard } from './hesap/guards/login.guard';

@NgModule({
  declarations: [
    GirisComponent,
    AppComponent,
    PersonelListesiComponent,
    YeniPersonelComponent,
    YeniYoklamaComponent,
    YoklamaListesiComponent,
    YoklamaDetayComponent,
    YapimAsamasindaComponent,
    SortPipe,
    SifreDegistirComponent,
    YoklamaBaslikListesiComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ErrorsModule
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
export class AppModule {}
