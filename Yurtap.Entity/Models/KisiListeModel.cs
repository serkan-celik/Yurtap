using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.Enums;

namespace Yurtap.Entity.Models
{
    public class KisiListeModel
    {
        public int KisiId { get; set; }
        public string AdSoyad { get; set; }
        public KisiEnum KisiTip { get; set; }
    }
}
