using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                return _ad.Trim();
            }
            set
            {
                _ad = value;
            }
        }
        private string _soyad;
        public string Soyad
        {
            get
            {
                return _soyad.Trim();
            }
            set
            {
                _soyad = value;
            }
        }
        public string TcKimlikNo { get; set; }
        public bool Hesap { get; set; }
    }
}
