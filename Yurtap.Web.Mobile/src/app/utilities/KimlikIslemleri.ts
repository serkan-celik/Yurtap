export class KimlikIslemleri {
    hataMetni: string = "";

    public tcKimlikNoDogrula(tcKimlikNo):string {
        var tek = 0,
            cift = 0,
            sonuc = 0,
            TCToplam = 0,
            i = 0;

        if (tcKimlikNo.length != 11) {
            return this.hataMetni = "";
            //return false;
        }
        if (isNaN(tcKimlikNo)) {
            return this.hataMetni = "Geçersiz tc no";
            //return false;
        }
        if (tcKimlikNo[0] == 0) { this.hataMetni = "Geçersiz tc no"; /*return false;*/ }

        tek = parseInt(tcKimlikNo[0]) + parseInt(tcKimlikNo[2]) + parseInt(tcKimlikNo[4]) + parseInt(tcKimlikNo[6]) + parseInt(tcKimlikNo[8]);
        cift = parseInt(tcKimlikNo[1]) + parseInt(tcKimlikNo[3]) + parseInt(tcKimlikNo[5]) + parseInt(tcKimlikNo[7]);

        tek = tek * 7;
        sonuc = Math.abs(tek - cift);
        if (sonuc % 10 != tcKimlikNo[9]) {
            return this.hataMetni = "Geçersiz tc no";
            //return false;
        }

        for (var i = 0; i < 10; i++) {
            TCToplam += parseInt(tcKimlikNo[i]);
        }

        if (TCToplam % 10 != tcKimlikNo[10]) {
            return this.hataMetni = "Geçersiz tc no";
            //return false;
        }

        return this.hataMetni = "";
        //return true;
    }
}