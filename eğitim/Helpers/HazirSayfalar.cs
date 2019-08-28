using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tabim.Core.Service.Helpers
{
    public class Sayfa
    {
        public string Ad { get; set; }
        public string Baglanti { get; set; }

        public Sayfa(string ad, string baglanti)
        {
            Ad = ad;
            Baglanti = baglanti;
        }

        public static List<Sayfa> getSayfalar()
        {
            List<Sayfa> sayfa = new List<Sayfa>();

            return new List<Sayfa>()
            {
                new Sayfa("Hakkında",@"/Home/Hakkinda"),
                new Sayfa("Iletişim",@"/Home/Iletisim"),
                new Sayfa("Personel",@"/Home/Personel"),
                new Sayfa("Galeri",@"/Home/Galeri"),
                new Sayfa("E-Sınav",@"/Home/Sinavlar")
            };

        }

    }

}
