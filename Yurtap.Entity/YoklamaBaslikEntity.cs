using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yurtap.Core.Entity;
using Yurtap.Core.Utilities.ExtensionMethods;

namespace Yurtap.Entity
{
    public class YoklamaBaslikEntity : EntityBase<byte>
    {
        private string _baslik;
        public string Baslik
        {
            get
            {
                return _baslik;
            }
            set
            {
                _baslik = value.Trim().ToTitleCase();
            }
        }
    }
}
