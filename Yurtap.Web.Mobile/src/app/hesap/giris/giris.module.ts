import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { IonicModule } from '@ionic/angular';
import { GirisComponent } from './giris.component';
import { PopModule } from 'src/app/popcomponent/pop-module.module';

const routes: Routes = [
  {
    path: '',
    component: GirisComponent
  }
];

/*@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    PopModule,
    RouterModule.forChild([
      {
        path: '',
        component: GirisComponent,
      }
    ])
  ],
    entryComponents:[GirisComponent],
  declarations: [GirisComponent]
})
export class GirisModule {}*/