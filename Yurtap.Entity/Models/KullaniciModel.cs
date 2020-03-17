using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity.Enums;

namespace Yurtap.Entity.Models
{
    public class KullaniciModel
    {
        public KullaniciModel()
        {
            Ad = "Admin";
            Sifre = "12345";
            AdSoyad = "Admin";
            Roller = new List<KullaniciRolModel>()
            {
                new KullaniciRolModel()
                {
                    RolId = RolEnum.GenelYonetici.GetHashCode(),
                    Ad = "Admin"
                }
            };
        }
        public int KisiId { get; set; }
        public string Ad { get; set; }
        public string Sifre { get; set; }
        public string AdSoyad { get; set; }
        public string Token { get; set; }
        public List<KullaniciRolModel> Roller { get; set; }
    }
}
