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

@NgModule({
  declarations: [
    AppComponent,
    YapimAsamasindaComponent,
    SortPipe],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
   AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ErrorsModule,
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
