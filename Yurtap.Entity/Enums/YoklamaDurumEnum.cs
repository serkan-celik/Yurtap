using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yurtap.Entity.Enums
{
    public enum YoklamaDurumEnum : byte
    {
        [Display(Name = "Yok", Description = "X")]
        Yok = 0,
        [Display(Name = "Var",Description ="+")]
        Var = 1,
        [Display(Name = "Okulda",Description ="O")]
        Okulda = 2,
        [Display(Name = "İzinli", Description = "İ")]
        Izinli = 3,
        [Display(Name = "Görevli", Description = "G")]
        Gorevli = 4,
        [Display(Name = "Hasta", Description = "H")]
        Rahatsiz = 5
    }
}
