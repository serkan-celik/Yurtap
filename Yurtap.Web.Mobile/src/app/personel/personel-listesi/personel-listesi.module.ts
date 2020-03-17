import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
import { PersonelListesiComponent } from './personel-listesi.component';
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
        component: PersonelListesiComponent,
      }
    ])
  ],
  entryComponents:[PersonelListesiComponent],
  declarations: [PersonelListesiComponent],
})
export class PersonelListesiModule { }
