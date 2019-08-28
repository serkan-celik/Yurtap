using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity.Enums;

namespace Yurtap.Entity.Models
{
    public class YoklamaModel
    {
        public int Id { get; set; }
        public int EkleyenId { get; set; }
        public byte YoklamaBaslikId { get; set; }
        public string Baslik { get; set; }
        public DateTime Tarih { get; set; }
        public List<YoklamaListeModel> YoklamaListesi { get; set; }
    }
}
