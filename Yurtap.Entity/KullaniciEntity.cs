using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class KullaniciEntity : EntityBase<int>
    {
        [Column("KisiId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
        public string Ad { get; set; }
        public string Sifre { get; set; }
    }
}
