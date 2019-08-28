using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class KullaniciRolEntity : EntityBase<int>
    {
        public int KisiId { get; set; }
        public int RolId { get; set; }
        public bool Ekleme { get; set; } = true;
        public bool Silme { get; set; } = true;
        public bool Guncelleme { get; set; } = true;
        public bool Listeleme { get; set; } = true;
        public bool Arama { get; set; } = true;
    }
}
