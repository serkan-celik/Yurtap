import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { RouterModule } from '@angular/router';
import { AnaSayfaComponent } from './ana-sayfa';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild([
      {
        path: '',
        component: AnaSayfaComponent
      }
    ])
  ],
  declarations: [AnaSayfaComponent]
})
export class AnaSayfaModule {}
