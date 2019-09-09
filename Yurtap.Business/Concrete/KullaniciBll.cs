
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Yurtap.Business.Abstract;
using Yurtap.Core.Entity.Enums;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Concrete
{
    public class KullaniciBll : BaseManager, IKullaniciBll
    {
        private readonly IKullaniciDal _kullaniciDal;
        public KullaniciBll(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }

        public KullaniciEntity AddKullanici(KullaniciModel kullaniciModel)
        {
            return _kullaniciDal.Add(new KullaniciEntity {
                Id = kullaniciModel.KisiId,
                Ad = kullaniciModel.Ad,
                Sifre = kullaniciModel.Sifre
            });
        }

        public bool DeleteKullanici(KullaniciEntity kullaniciEntity)
        {
            return _kullaniciDal.Delete(kullaniciEntity) > 0 ? true : false;
        }

        public KullaniciModel GetKullaniciBilgileri(string kullaniciAdi, string kullaniciSifre)
        {
            return _kullaniciDal.GetKullaniciBilgileri(kullaniciAdi, kullaniciSifre);
        }

        public List<KullaniciModel> GetKullaniciListesi()
        {
            return _kullaniciDal.GetKullaniciListesi().ToList();
        }

        public bool IsKullanici(string kullaniciAd)
        {
            if(_kullaniciDal.Any(k=>k.Ad == kullaniciAd && k.Id != CurrentUser.Id))
            {
                throw new Exception("Kullanıcı adı kullanılıyor!");
            }
            return false;
        }

        public KullaniciEntity UpdateKullanici(KullaniciModel kullaniciModel)
        {
            return _kullaniciDal.Update(new KullaniciEntity
            {
                Id = kullaniciModel.KisiId,
                Ad = kullaniciModel.Ad,
                Sifre = kullaniciModel.Sifre,
                SonGuncelleyenId = CurrentUser.Id,
                SonGuncellemeTarihi = DateTime.Now
            });
        }

        public bool IsKullanici(int kisiId)
        {
            return _kullaniciDal.Any(k => k.Id == kisiId && k.Durum == DurumEnum.Aktif);
        }

        public KullaniciEntity UpdateKullanici(KullaniciEntity kullaniciEntity)
        {
            kullaniciEntity.SonGuncelleyenId = CurrentUser.Id;
            kullaniciEntity.SonGuncellemeTarihi = DateTime.Now;
            return _kullaniciDal.Update(kullaniciEntity);
        }

        public KullaniciEntity GetKullaniciById(int kisiId)
        {
            return _kullaniciDal.Get(k => k.Id == kisiId);
        }
    }
}
