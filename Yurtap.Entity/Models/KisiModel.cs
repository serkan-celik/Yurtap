using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace Yurtap.Entity.Models
{
    public class KisiModel
    {
        public int KisiId { get; set; }
        private string _ad;
        public string Ad {
            get
            {
                return _ad;
            }
            set
            {
                _ad = value.Trim().ToUpper(new CultureInfo("tr-TR"));
            }
        }
        private string _soyad;
        public string Soyad
        {
            get
            {
                return _soyad;
            }
            set
            {
                _soyad = value.Trim().ToUpper(new CultureInfo("tr-TR"));
            }
        }
        public string TcKimlikNo { get; set; }
        public bool Hesap { get; set; }
        public string KullaniciAd { get; set; }
        public string Sifre { get; set; }
    }
}
