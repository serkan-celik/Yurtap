using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class YoklamaEntity : EntityBase<int>
    {
        public byte YoklamaBaslikId { get; set; }
        public DateTime Tarih { get; set; }
        public string Liste { get; set; }
    }
}
