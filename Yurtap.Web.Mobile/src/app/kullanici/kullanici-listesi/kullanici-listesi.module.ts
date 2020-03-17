import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
import { KullaniciListesiComponent } from './kullanici-listesi.component';
import { TextAvatarModule } from 'src/app/components/text-avatar';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    TextAvatarModule,
    RouterModule.forChild([
      {
        path: '',
        component: KullaniciListesiComponent
      }
    ])
  ],
  entryComponents:[KullaniciListesiComponent],
  declarations: [KullaniciListesiComponent]
})
export class KullaniciListesiModule {}
