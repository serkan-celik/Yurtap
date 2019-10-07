import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { AnaSayfaComponent } from './ana-sayfa';
import { PopModule } from '../popcomponent/pop-module.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: AnaSayfaComponent,
      }
    ])
  ],
  entryComponents:[AnaSayfaComponent],
  declarations: [AnaSayfaComponent],
})
export class AnaSayfaModule { }
