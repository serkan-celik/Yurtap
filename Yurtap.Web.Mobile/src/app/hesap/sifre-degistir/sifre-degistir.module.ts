import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { SifreDegistirComponent } from './sifre-degistir.component';
import { PopModule } from 'src/app/popcomponent/pop-module.module';

const routes: Routes = [
  {
    path: '',
    component: SifreDegistirComponent
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: SifreDegistirComponent,
      }
    ])
  ],
  entryComponents:[SifreDegistirComponent],
  declarations: [SifreDegistirComponent]
})
export class SifreDegistirModule {}