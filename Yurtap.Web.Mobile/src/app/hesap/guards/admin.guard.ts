
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterState, RouterStateSnapshot, Router } from '@angular/router';
import { HesapService } from 'src/app/services/hesap/hesap.service';
import { Rol } from 'src/app/models/account/CurrentUser';
import { RolEnum } from 'src/app/enums/RolEnum';
import { BaseComponent } from 'src/app/BaseComponent';

@Injectable()
export class AdminGuard  extends BaseComponent implements CanActivate {//CanActivate: gitmek istediği sayfa active edebilmiş mi? true:false
    constructor(public hesapService: HesapService, private router: Router) {
        super(hesapService);
    }

    canActivate(
        next: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): boolean {//gelinen sayfa
        let isLoggedIn = this.hesapService.isLoggedIn;

        if (this.getUserRoles) {
            for (let index = 0; index < this.getUserRoles.length; index++) {
                if (isLoggedIn && (this.getUserRoles[index].rolId == RolEnum.Admin)){
                    return true;
                }
            }
        }
        this.router.navigate(["hesap/giris"], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
