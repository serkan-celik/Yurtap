import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/BaseComponent';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { KullaniciService } from 'src/app/services/kullanici.service';
import { Rol } from 'src/app/models/account/CurrentUser';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from 'src/app/services/toast/toast.service';
import { AlertService } from 'src/app/services/alert/alert.service';

@Component({
  selector: 'app-yeni-yetki',
  templateUrl: './yeni-yetki.component.html',
  styleUrls: ['./yeni-yetki.component.scss'],
})
export class YeniYetkiComponent extends BaseComponent implements OnInit {

  constructor(
    public hesapService: HesapService,
    private kullaniciService: KullaniciService,
    private kisiService: KullaniciService,
    private activatedRoute: ActivatedRoute, 
    private toastService: ToastService,
    private alertService:AlertService) {
    super(hesapService);
  }
  roller: Rol[] = []
  rol: Rol = new Rol();
  kisiId: number;
  kisiAdSoyad:string

  ngOnInit() {
    this.getkullaniciRolleri();
  }

  getkullaniciRolleri() {
    this.activatedRoute.params.subscribe(param => {
      if (param.kisiId && param.adSoyad) {
        this.kisiId = param.kisiId;
        this.kullaniciService.getKullaniciRolleriById(param.kisiId).subscribe(data => {
          this.roller = data;
          this.kisiAdSoyad = param.adSoyad;
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
        this.toastService.showToast("Kullanıcı yetkisi oluşturuldu")
    });

  }

rolSil(rol) {
    this.alertService.confirmDeleteAlert("<b>" + rol.ad + "</b> yetkisi silinsin mi?", this.kullaniciService.deleteKullaniciRol(rol), this.roller, rol, "Kullanıcı yetkisi silindi");
  }

  rolGuncelle(rol:Rol){
    rol.kisiId = this.kisiId;
    this.kullaniciService.updateKullaniciRol(rol).subscribe(data=>{
      if(data)
      this.toastService.showToast("Kullanıcı yetkisi güncellendi")
    })
  }
}
