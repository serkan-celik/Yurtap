
import { Observable, observable } from 'rxjs';
import { OgrenciListe } from '../models/OgrenciListe';
import { Ogrenci } from '../models/Ogrenci';
import { YoklamaListesi } from '../models/YoklamaListesi';

export class DummyYoklamaListesi{

  private dummyYoklamaListesi:OgrenciListe[]  = 
  [{"kisiId":2019,"ad":"ADEM","soyad":"BARAN","tcKimlikNo":"23814578794"},{"kisiId":2018,"ad":"ADEM","soyad":"BÜYÜK","tcKimlikNo":"61172550502"},{"kisiId":27,"ad":"AHMET","soyad":"KÜÇÜKOĞLU","tcKimlikNo":"39186595512"},{"kisiId":26,"ad":"AHMET","soyad":"KUTLUAY BÜKÜLMEZ","tcKimlikNo":"11179947560"},{"kisiId":28,"ad":"ALTUĞHAN","soyad":"ARSLAN","tcKimlikNo":"95036967588"},{"kisiId":29,"ad":"ARDA","soyad":"KILIÇ","tcKimlikNo":"70083374046"},{"kisiId":30,"ad":"ATABERK","soyad":"AYRIM","tcKimlikNo":"59935767168"},{"kisiId":1007,"ad":"BATUHAN","soyad":"KILIÇ","tcKimlikNo":"45994007086"},{"kisiId":1008,"ad":"BERKAY","soyad":"ÖZBİLGİN","tcKimlikNo":"47819417810"},{"kisiId":31,"ad":"BURAK","soyad":"KURU","tcKimlikNo":"58120544706"},{"kisiId":16,"ad":"EREN","soyad":"ADIGÜZEL","tcKimlikNo":"23511658102"},{"kisiId":17,"ad":"ERKUT","soyad":"AÇIKGÖZ","tcKimlikNo":"60763093688"},{"kisiId":18,"ad":"FETHİ","soyad":"ŞAHİNER","tcKimlikNo":"35376264466"},{"kisiId":1002,"ad":"GÖRKEM","soyad":"ÖZNEHİR","tcKimlikNo":"10020363184"},{"kisiId":1003,"ad":"GÜRKAN","soyad":"GÖKMEN","tcKimlikNo":"21363953956"},{"kisiId":19,"ad":"HALİL","soyad":"OZAN SEVİM","tcKimlikNo":"14835420872"},{"kisiId":1004,"ad":"HASAN","soyad":"BERKAY SEÇGİN","tcKimlikNo":"63491788130"},{"kisiId":1005,"ad":"HASAN","soyad":"BOZ","tcKimlikNo":"44270296442"},{"kisiId":1006,"ad":"HASAN","soyad":"HÜSEYİN GÖNCÜ","tcKimlikNo":"11819032960"},{"kisiId":2007,"ad":"HATİCE","soyad":"ÇELİK","tcKimlikNo":"60199068830"},{"kisiId":20,"ad":"KORAY","soyad":"ÖZTÜRK","tcKimlikNo":"94047918732"},{"kisiId":21,"ad":"MUSTAFA","soyad":"ŞAMİL FUTTU","tcKimlikNo":"50448610064"},{"kisiId":22,"ad":"MUSTAFA","soyad":"YAVUZ","tcKimlikNo":"30490544852"},{"kisiId":2016,"ad":"SAMET","soyad":"YERLİ","tcKimlikNo":"14002541232"},{"kisiId":23,"ad":"SERKAN","soyad":"DİNGEÇ","tcKimlikNo":"51620711968"},{"kisiId":24,"ad":"TAHİR","soyad":"ÇİFTÇİ","tcKimlikNo":"75844832810"},{"kisiId":25,"ad":"ÜMİT","soyad":"FURKAN TATLI","tcKimlikNo":"64757391934"}]

getDummyYoklamaListesi() : Observable<YoklamaListesi[]> {
    return Observable.create( observer => {
        observer.next(this.dummyYoklamaListesi);
        observer.complete();
    });}
}
