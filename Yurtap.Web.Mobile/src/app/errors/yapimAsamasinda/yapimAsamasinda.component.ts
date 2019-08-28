import { Component, OnInit } from '@angular/core';
//import { ModuleType } from 'src/app/model/soruListesi';
import { Router } from '@angular/router';
//import { WebSiteService } from 'src/app/services/webSite.service';

@Component({
  selector: 'app-yapimAsamasinda',
  templateUrl: './yapimAsamasinda.component.html',
  styleUrls: ['./yapimAsamasinda.component.css']
})
export class YapimAsamasindaComponent implements OnInit {

  constructor(/*private webSiteService:WebSiteService,*/ private router:Router) { }

  ngOnInit() {
    //this.isDomainModule();
  }

 /* isDomainModule() {
    this.webSiteService.isDomainModule(ModuleType.SMS).subscribe(data => {
      if(!data){
        this.router.navigateByUrl("/yonetim-module");
        return;
      }
    });
  }*/
}
