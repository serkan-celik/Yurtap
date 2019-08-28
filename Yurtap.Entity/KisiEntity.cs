using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Yurtap.Core.Entity;

namespace Yurtap.Entity
{
    public class KisiEntity : EntityBase<int>
    {
        private string _ad;
        public string Ad {
            get
            {
                return _ad;
            }
            set
            {
                _ad = value.Trim().ToUpper();
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
                _soyad = value.Trim().ToUpper();
            }
        }
        public string TcKimlikNo { get; set; }
    }
}
