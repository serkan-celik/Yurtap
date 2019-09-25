using System;
using System.Collections.Generic;
using System.Text;

namespace Yurtap.Model.ReportModels.YoklamaModels
{
    public class YoklamaAylikYuzdelikKatilimModel
    {
        public string AdSoyad { get; set; }
        public byte YoklamaBaslik { get; set; }
        public List<YoklamaIstatistik> YoklamaIstatistikleri { get; set; }
        public string Katilim { get; set; }
        public int YoklamaSayisi { get; set; }
        public int KatilimSayisi { get; set; }
        public string GenelKatilimYuzdesi { get; set; }
    }

    public class YoklamaIstatistik
    {
        public byte YoklamaBaslik { get; set; }
        public string KatilimYuzdesi { get; set; }
    }
}