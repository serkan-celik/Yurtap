import { Injectable } from '@angular/core';
import { HttpService } from './http.service';
import { environment } from 'src/environments/environment';
import { YoklamaBaslik } from '../models/YoklamaBaslik';

@Injectable({
    providedIn: "root"
})
export class YoklamaBaslikService {
    constructor(private httpService: HttpService) {
    }

    getYoklamaBaslikListesi() {
        return this.httpService.getHttp<any>(environment.yoklamaBaslikPath, "getYoklamaBaslikListesi");
    }
    addYoklamaBaslik(yoklamaBaslik:YoklamaBaslik) {
        return this.httpService.postHttp<any>(environment.yoklamaBaslikPath, "addYoklamaBaslik",yoklamaBaslik);
    }
    updateYoklamaBaslik(yoklamaBaslik:YoklamaBaslik) {
        return this.httpService.putHttp<any>(environment.yoklamaBaslikPath, "updateYoklamaBaslik",yoklamaBaslik);
    }
    deleteYoklamaBaslik(yoklamaBaslik:YoklamaBaslik) {
        return this.httpService.deleteHttp<any>(environment.yoklamaBaslikPath, "deleteYoklamaBaslik",yoklamaBaslik);
    }
}