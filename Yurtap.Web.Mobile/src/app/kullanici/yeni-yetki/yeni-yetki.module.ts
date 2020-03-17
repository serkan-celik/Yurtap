import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
import { YeniYetkiComponent } from './yeni-yetki.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: YeniYetkiComponent
      }
    ])
  ],
  entryComponents:[YeniYetkiComponent],
  declarations: [YeniYetkiComponent]
})
export class YeniYetkiModule {}
