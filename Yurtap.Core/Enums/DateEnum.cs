using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yurtap.Core.Enums
{
    public enum MonthsEnum : byte
    {
        [Display(Name = "Ocak")]
        Ocak = 1,
        [Display(Name = "Şubat")]
        Subat = 2,
        [Display(Name = "Mart")]
        Mart = 3,
        [Display(Name = "Nisan")]
        Nisan = 4,
        [Display(Name = "Mayıs")]
        Mayis = 5,
        [Display(Name = "Haziran")]
        Haziran = 6,
        [Display(Name = "Temmuz")]
        Temmuz = 7,
        [Display(Name = "Ağustos")]
        Agustos = 8,
        [Display(Name = "Eylül")]
        Eylul = 9,
        [Display(Name = "Ekim")]
        Ekim = 10,
        [Display(Name = "Kasım")]
        Kasim = 11,
        [Display(Name = "Aralık")]
        Aralik = 12
    }

    public enum DaysEnum : byte
    {
        [Display(Name = "Pazar")]
        Pazar = 0,
        [Display(Name = "Pazartesi")]
        Pazartesi = 1,
        [Display(Name = "Salı")]
        Salı = 2,
        [Display(Name = "Çarşamba")]
        Çarsamba = 3,
        [Display(Name = "Perşembe")]
        Persembe = 4,
        [Display(Name = "Cuma")]
        Cuma = 5,
        [Display(Name = "Cumartesi")]
        Cumartesi = 6
    }
}
