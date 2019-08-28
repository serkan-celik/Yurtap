using System;
using System.Collections.Generic;
using System.Text;

namespace Yurtap.Entity.Models
{
    public class KullaniciRolModel
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int RolId { get; set; }
        public bool Ekleme { get; set; }
        public bool Silme { get; set; }
        public bool Guncelleme { get; set; }
        public bool Listeleme { get; set; }
        public bool Arama { get; set; }
    }
}
