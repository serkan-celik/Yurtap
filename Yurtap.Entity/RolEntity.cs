using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class RolEntity : EntityBase<int>
    {
        public string Ad { get; set; }
    }
}
