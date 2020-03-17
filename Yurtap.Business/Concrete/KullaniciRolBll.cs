using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
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
        private readonly IKullaniciBll _kullaniciBll;
        public KullaniciRolBll(IKullaniciRolDal kullaniciRolDal, IKullaniciBll kullaniciBll)
        {
            _kullaniciRolDal = kullaniciRolDal;
            _kullaniciBll = kullaniciBll;
        }
        public KullaniciRolEntity AddKullaniciRol(KullaniciRolEntity kullaniciRolEntity)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var isKullanici = _kullaniciBll.GetKullaniciById(kullaniciRolEntity.KisiId);
                if (isKullanici == null)
                {
                    var kullanici = _kullaniciBll.AddGenerateKullanici(kullaniciRolEntity);
                }
                IsKullaniciRol(kullaniciRolEntity.KisiId, kullaniciRolEntity.RolId);
                var kullaniciRol = _kullaniciRolDal.Add(kullaniciRolEntity);
                scope.Complete();
                return kullaniciRol;
            }
        }

        public bool DeleteKullaniciRol(KullaniciRolEntity kullaniciRolEntity)
        {
            return _kullaniciRolDal.Delete(kullaniciRolEntity) > 0 ? true : false;
        }

        public List<KullaniciRolModel> GetKullaniciRolleriById(int kisiId)
        {
            return _kullaniciRolDal.GetList(r => r.KisiId == kisiId && r.Durum == DurumEnum.Aktif).Select(kr => new KullaniciRolModel
            {
                Id = kr.Id,
                Ad = ((RolEnum)Enum.Parse(typeof(RolEnum), kr.RolId.ToString())).GetDisplayName(),
                RolId = kr.RolId,
                Ekleme = kr.Ekleme,
                Silme = kr.Silme,
                Guncelleme = kr.Guncelleme,
                Arama = kr.Arama,
                Listeleme = kr.Listeleme
            }).OrderBy(r=>r.Ad).ToList();
        }

        public List<KullaniciRolListeModel> GetKullaniciRolleriListesi()
        {
            return _kullaniciRolDal.GetKullaniciRolleriListesi().ToList();
        }

        public bool IsKullaniciRol(int kisiId, int rolId)
        {
            if (_kullaniciRolDal.Any(r => r.KisiId == kisiId && r.RolId == rolId))
            {
                throw new Exception("Yetki daha önceden verilmiştir");
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
