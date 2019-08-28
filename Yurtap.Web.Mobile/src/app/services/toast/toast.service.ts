import { Injectable } from '@angular/core';
import { ToastController } from '@ionic/angular';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private toastController: ToastController) { }

  public async showToast(
    message: string, 
    color:string="dark",
    position: 'top' | 'bottom' | 'middle' = "bottom", 
    duration: number = 2000,
    showCloseButton:boolean = false,
    closeButtonText:string = "Tamam"
    ) {
    const toast = await this.toastController.create({
      message: message,
      color:color,
      position: position,
      duration: duration,
      showCloseButton: showCloseButton,
      closeButtonText: closeButtonText,
      keyboardClose: true
    });
    toast.present();
  }
}
