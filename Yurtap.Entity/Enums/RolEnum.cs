using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Yurtap.Entity.Enums
{
    public enum RolEnum : byte
    {
        [Display(Name = "Genel Yönetici")]
        GenelYonetici = 1,
        [Display(Name = "Yoklama Yöneticisi")]
        YoklamaYoneticisi = 2,
        [Display(Name = "Öğrenci Yöneticisi")]
        OgrenciYoneticisi = 3,
        [Display(Name = "Personel Yöneticisi")]
        PersonelYoneticisi = 4
    }
}
