import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/BaseComponent';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { KullaniciService } from 'src/app/services/kullanici.service';
import { Rol } from 'src/app/models/account/CurrentUser';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/services/toast/toast.service';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-ogrenci-yetki',
  templateUrl: './ogrenci-yetki.component.html',
  styleUrls: ['./ogrenci-yetki.component.scss'],
})
export class OgrenciYetkiComponent extends BaseComponent implements OnInit {

  constructor(
    public hesapService: HesapService,
    private kullaniciService: KullaniciService,
    private activatedRoute: ActivatedRoute, 
    private toastService: ToastService,
    private alertService:AlertService) {
    super(hesapService);
  }
  roller: Rol[] = []
  rol: Rol = new Rol();
  kisiId: number;

  ngOnInit() {
    this.getkullaniciRolleri();
  }

  getkullaniciRolleri() {
    this.activatedRoute.params.subscribe(param => {
      if (param.kisiId) {
        this.kisiId = param.kisiId;
        this.kullaniciService.getKullaniciRolleriById(param.kisiId).subscribe(data => {
          this.roller = data;
        })
      }
    })
  }

  rolEkle(rol: Rol) {
    if (!rol.rolId) {
      this.toastService.showToast("Lütfen yetki seçiniz");
      return;
    }
    rol.kisiId = this.kisiId;
    this.kullaniciService.addKullaniciRol(rol).subscribe(data => {
      if (data)
        this.getkullaniciRolleri();
        this.toastService.showToast("Kullanıcı yetkisi eklendi")
    });

  }

rolSil(rol) {
    this.alertService.confirmDeleteAlert(rol.ad + " yetkisi silinsin mi?", this.kullaniciService.deleteKullaniciRol(rol), this.roller, rol, "Kullanıcı rolü silindi");
  }

  rolGuncelle(rol:Rol){
    rol.kisiId = this.kisiId;
    this.kullaniciService.updateKullaniciRol(rol).subscribe(data=>{
      if(data)
      this.toastService.showToast("Kullanıcı yetkisi güncellendi")
    })
  }
}
