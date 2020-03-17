import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { OgrenciListesiComponent } from './ogrenci-listesi';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
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
        component: OgrenciListesiComponent,
      }
    ])
  ],
  entryComponents:[OgrenciListesiComponent],
  declarations: [OgrenciListesiComponent],
})
export class OgrenciListesiModule { }
