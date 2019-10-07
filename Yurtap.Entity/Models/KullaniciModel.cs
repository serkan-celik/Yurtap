using System;
using System.Collections.Generic;
using System.Text;

namespace Yurtap.Entity.Models
{
    public class KullaniciModel
    {
        public int KisiId { get; set; }
        public string Ad { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string Token { get; set; }
        public List<KullaniciRolModel> Roller { get; set; }
    }
}
