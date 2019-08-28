import { YoklamaListesi } from './YoklamaListesi';
import * as moment from 'moment';
export class Yoklama { 
    id:number;
    ekleyenId:number;
    yoklamaBaslikId: number;
    baslik:string;
    tarih: string = moment().toLocaleString();
    yoklamaListesi: YoklamaListesi[]=[];
}