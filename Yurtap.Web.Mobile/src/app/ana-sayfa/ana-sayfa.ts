import { Component } from '@angular/core';
import { HesapService } from '../services/hesap/hesap.service';
import { CurrentUser } from '../models/account/CurrentUser';
import { BaseComponent } from '../BaseComponent';
import { PopoverController } from '@ionic/angular';
import { PopoverComponent } from '../popover/popover.component';

@Component({
  selector: 'app-home',
  templateUrl: 'ana-sayfa.html',
  styleUrls: ['ana-sayfa.scss']
})
export class AnaSayfaComponent extends BaseComponent {
  constructor(public hesapService: HesapService) {
    super(hesapService);
  }


}