import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { PopModule } from 'src/app/popcomponent/pop-module.module';
import { YoklamaListeleriComponent } from './yoklama-listeleri.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: YoklamaListeleriComponent,
      }
    ])
  ],
  declarations: [YoklamaListeleriComponent],
})
export class YoklamaListeleriModule { }
