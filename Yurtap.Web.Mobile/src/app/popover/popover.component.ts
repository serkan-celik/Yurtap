import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../BaseComponent';
import { HesapService } from '../services/hesap/hesap.service';
import { PopoverController } from '@ionic/angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-popover',
  templateUrl: './popover.component.html',
  styleUrls: ['./popover.component.scss'],
})
export class PopoverComponent  extends BaseComponent {

  constructor(
    public hesapService: HesapService,
    public popoverController: PopoverController,
    public router:Router) {
    super(hesapService);
  }

  cikisYap() {
    this.hesapService.logOut();
    this.popoverController.dismiss();
  }

  sifreDegistir(){
    this.router.navigateByUrl("sifre-degistir");
    this.popoverController.dismiss();
  }
}
