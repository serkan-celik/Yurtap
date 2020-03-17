export class CurrentUser {
    id: number;
    name: string;
    password: string;
    fullName: string = "Misafir";
    isLogged: boolean;
    roles: Rol[]=[];
}

export class Rol {
    id:number;
    ad:string;
    rolId: number;
    kisiId:number;
    ekleme: boolean;
    silme: boolean;
    guncelleme: boolean;
    listeleme: boolean;
    arama: boolean;
}

export class UserRoleList {
    kisiId:number;
    adSoyad:string;
    kullaniciAd:string;
    sifre:string;
    roller: string[]=[];
}