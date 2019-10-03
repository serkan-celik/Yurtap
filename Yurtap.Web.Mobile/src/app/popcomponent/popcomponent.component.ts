import { Component, OnInit } from '@angular/core';
import { PopoverComponent } from '../popover/popover.component';
import { PopoverController, Platform } from '@ionic/angular';
import { BaseComponent } from '../BaseComponent';
import { HesapService } from '../services/hesap/hesap.service';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';

@Component({
  selector: 'pop-account-component',
  templateUrl: './popcomponent.component.html',
  styleUrls: ['./popcomponent.component.scss'],
})
export class PopcomponentComponent extends BaseComponent {

  constructor(public hesapService: HesapService,
    public popoverController: PopoverController,
    public platform: Platform,

  ) {
    super(hesapService);
    this.initializeApp();
  }
  isDesktop: boolean = false;

  initializeApp() {
    this.platform.ready().then(() => {
      this.isDesktop = this.platform.is("desktop");
    });
  }


  async presentPopover(ev: any) {
    const popover = await this.popoverController.create({
      component: PopoverComponent,
      event: ev,
      translucent: true
    });

    if (this.hesapService.isLoggedIn) {
      return await popover.present();
    }
    else {
      return await popover.dismiss();
    }
  }
}
