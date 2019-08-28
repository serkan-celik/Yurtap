using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class OgrenciEntity : EntityBase<int>
    {
        [Column("KisiId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }
    }
}
