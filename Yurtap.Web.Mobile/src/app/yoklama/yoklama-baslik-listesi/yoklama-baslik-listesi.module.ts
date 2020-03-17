import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
import { YoklamaBaslikListesiComponent } from './yoklama-baslik-listesi.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild([
      {
        path: '',
        component: YoklamaBaslikListesiComponent,
      }
    ])
  ],
  entryComponents:[YoklamaBaslikListesiComponent],
  declarations: [YoklamaBaslikListesiComponent],
})
export class YoklamaBaslikListesiModule { }
