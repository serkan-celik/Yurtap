using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity.Enums;

namespace Yurtap.Entity.Models
{
    public class KullaniciRolModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int RolId { get; set; }
        public bool Ekleme { get; set; } = true;
        public bool Silme { get; set; } = true;
        public bool Guncelleme { get; set; } = true;
        public bool Listeleme { get; set; } = true;
        public bool Arama { get; set; } = true;
    }
}
