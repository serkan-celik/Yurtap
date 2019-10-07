import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { YeniOgrenciComponent } from './yeni-ogrenci.component';
import { PopModule } from 'src/app/popcomponent/pop-module.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: YeniOgrenciComponent
      }
    ])
  ],
  entryComponents:[YeniOgrenciComponent],
  declarations: [YeniOgrenciComponent]
})
export class YeniOgrenciModule {}
