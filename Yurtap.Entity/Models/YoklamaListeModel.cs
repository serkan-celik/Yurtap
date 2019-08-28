using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity.Enums;

namespace Yurtap.Entity.Models
{
    public class YoklamaListeModel
    {
        public string KisiId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Durum { get; set; }
        public int EkleyenId { get; set; }
        public YoklamaDurumEnum YoklamaDurum { get; set; }
    }
}
