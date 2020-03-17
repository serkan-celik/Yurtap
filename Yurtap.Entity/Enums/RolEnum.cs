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
        [Display(Name = "Yoklama Yönetimi")]
        YoklamaYoneticisi = 2,
        [Display(Name = "Öğrenci Yönetimi")]
        OgrenciYoneticisi = 3,
        [Display(Name = "Personel Yönetimi")]
        PersonelYoneticisi = 4
    }
}
