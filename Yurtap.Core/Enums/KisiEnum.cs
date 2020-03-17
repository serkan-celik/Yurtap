using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yurtap.Core.Enums
{
    public enum KisiEnum
    {
        [Display(Name = "Personel")]
        Personel = 0,
        [Display(Name = "Öğrenci")]
        Ogrenci = 1
    }
}
