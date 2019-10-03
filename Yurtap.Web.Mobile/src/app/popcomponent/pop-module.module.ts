import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IonicModule } from '@ionic/angular';
import { PopoverComponent } from '../popover/popover.component';
import { PopcomponentComponent } from './popcomponent.component';
import { AppRoutingModule } from '../app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule,
  ],
  exports:[PopoverComponent,PopcomponentComponent],
  entryComponents: [PopoverComponent],
  declarations: [PopoverComponent,PopcomponentComponent],
})
export class PopModule { }
