using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Entity.Enums;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Enums;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Concrete
{
    public class KullaniciRolBll : BaseManager, IKullaniciRolBll
    {
        private readonly IKullaniciRolDal _kullaniciRolDal;
        public KullaniciRolBll(IKullaniciRolDal kullaniciRolDal)
        {
            _kullaniciRolDal = kullaniciRolDal;
        }
        public KullaniciRolEntity AddKullaniciRol(KullaniciRolEntity kullaniciRolEntity)
        {
            IsKullaniciRol(kullaniciRolEntity.KisiId, kullaniciRolEntity.RolId);
                return _kullaniciRolDal.Add(kullaniciRolEntity);
        }

        public bool DeleteKullaniciRol(KullaniciRolEntity kullaniciRolEntity)
        {
            return _kullaniciRolDal.Delete(kullaniciRolEntity) > 0 ? true : false;
        }

        public List<KullaniciRolModel> GetKullaniciRolleriById(int kisiId)
        {
            return _kullaniciRolDal.GetList(r => r.KisiId == kisiId && r.Durum == DurumEnum.Aktif).Select(kr => new KullaniciRolModel()
            {
                Id = kr.Id,
                Ad = ((RolEnum)Enum.Parse(typeof(RolEnum), kr.RolId.ToString())).GetDisplayName(),
                RolId = kr.RolId,
                Ekleme = kr.Ekleme,
                Silme = kr.Silme,
                Guncelleme = kr.Guncelleme,
                Arama = kr.Arama,
                Listeleme = kr.Listeleme
            }).OrderBy(r=>r.RolId).ToList();
        }

        public bool IsKullaniciRol(int kisiId, int rolId)
        {
            if (_kullaniciRolDal.Any(r => r.KisiId == kisiId && r.RolId == rolId))
            {
                throw new Exception("Yetki daha önceden kayıtlıdır");
            }
            return true;
        }

        public KullaniciRolEntity UpdateKullaniciRol(KullaniciRolEntity kullaniciRolEntity)
        {
            kullaniciRolEntity.SonGuncelleyenId = CurrentUser.Id;
            kullaniciRolEntity.SonGuncellemeTarihi = DateTime.Now;
            return _kullaniciRolDal.Update(kullaniciRolEntity);
        }
    }
}
