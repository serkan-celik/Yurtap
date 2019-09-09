import { Injectable } from '@angular/core';
import { AlertController } from '@ionic/angular';
import { AlertInput, AlertButton } from '@ionic/core';
import { HttpService } from '../http.service';
import { Observable } from 'rxjs';
import { ToastService } from '../toast/toast.service';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor(private alertController: AlertController, private toastService: ToastService) { }

  async basicAlert(message: string) {
    const alert = await this.alertController.create(
      {
        header: "Uyarı",
        message: message,
        buttons: [
          {
            text: "Tamam"
          }
        ]
      }
    );
    await alert.present()
  }

  async confirmDeleteAlert(message: string, method: Observable<any>, modelList: any[], model: any, successMessage: string) {
    const alert = await this.alertController.create(
      {
        message: message,
        buttons: [
          {
            text: "Evet",
            handler: () => {
              method.subscribe(data => {
                if (data) {
                  var index = modelList.findIndex(m => m == model)
                  modelList.splice(index, 1);
                  this.toastService.showToast(successMessage)
                }
              })
            }
          },
          {
            text: "Hayır"
          }
        ]
      }
    );
    await alert.present()
  }

  async confirmAlert(questionMessage: string, method: Observable<any>, successMessage: string) {
    const alert = await this.alertController.create(
      {
        message: questionMessage,
        buttons: [
          {
            text: "Evet",
            handler: () => {
              method.subscribe(data => {
                if (data) {
                  this.toastService.showToast(successMessage);
                }
              })
            }
          },
          {
            text: "Hayır"
          }
        ]
      }
    );
    await alert.present()
  }

  async advancedAlert(header: string, message: string, subHeader?: string, ) {
    const alert = await this.alertController.create(
      {
        header: header,
        subHeader: subHeader,
        message: message,
        buttons: [
          {
            text: "Tamam"
          }
        ]
      }
    );
    await alert.present()
  }

  async inputAlert(
    header: string,
    inputs: AlertInput[],
    buttons: AlertButton[],
    message?: string,
    subHeader?: string
  ) {

    if (buttons.length == 0)
      buttons.push({
        text: "Tamam"
      })

    const alert = await this.alertController.create(
      {
        header: header,
        subHeader: subHeader,
        message: message,
        inputs: inputs,
        buttons: buttons
      }
    );
    await alert.present()
  }
}
