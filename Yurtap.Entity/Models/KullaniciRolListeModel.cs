using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.Enums;

namespace Yurtap.Entity.Models
{
    public class KullaniciRolListeModel
    {
        public int KisiId { get; set; }
        public string AdSoyad { get; set; }
        public string KullaniciAd { get; set; }
        public string Sifre { get; set; }
        public string[] Roller { get; set; }
    }
}
