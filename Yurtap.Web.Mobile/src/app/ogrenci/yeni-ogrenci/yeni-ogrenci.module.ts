import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { YeniOgrenciComponent } from './yeni-ogrenci.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild([
      {
        path: '',
        component: YeniOgrenciComponent
      }
    ])
  ],
  declarations: [YeniOgrenciComponent]
})
export class YeniOgrenciModule {}
