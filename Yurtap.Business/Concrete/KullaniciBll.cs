
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Yurtap.Business.Abstract;
using Yurtap.Core.Business.Models;
using Yurtap.Core.Entity.Enums;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Concrete
{
    public class KullaniciBll : BaseManager, IKullaniciBll
    {
        private readonly IKullaniciDal _kullaniciDal;
        private readonly IKullaniciRolDal _kullaniciRolDal;
        private readonly IKisiBll _kisiBll;
        public KullaniciBll(IKullaniciDal kullaniciDal,IKullaniciRolDal kullaniciRolDal, IKisiBll kisiBll)
        {
            _kullaniciDal = kullaniciDal;
            _kullaniciRolDal = kullaniciRolDal;
            _kisiBll = kisiBll;
        }

        public KullaniciEntity AddGenerateKullanici(KullaniciRolEntity kullaniciRolEntity)
        {
            var kisi = _kisiBll.GetKisi(kullaniciRolEntity.KisiId);
            string ad = kisi.Result.Ad.RemoveEmpty();
            string soyad = kisi.Result.Soyad;
            string defaultKullaniciAd = string.Join('.', ad, soyad).ToLower(new CultureInfo("tr-TR")); //ToLower(new CultureInfo("en-US")).ConvertTRCharToENChar();//
            string defaultSifre = new Random().Next(1234, 9999).ToString();

            while (IsUsedSifre(defaultSifre))
            {
                defaultSifre = new Random().Next(1234, 9999).ToString();
            }
            return _kullaniciDal.Add(new KullaniciEntity
            {
                Id = kullaniciRolEntity.KisiId,
                Ad = defaultKullaniciAd,
                Sifre = defaultSifre,
            });
        }

        public bool DeleteKullanici(KullaniciRolListeModel kullaniciRolListeModel)
        {
            var kullanici = _kullaniciDal.Get(k => k.Id == kullaniciRolListeModel.KisiId && k.Durum == DurumEnum.Aktif);
            var kullaniciRolleri = _kullaniciRolDal.GetList(k => k.KisiId == kullaniciRolListeModel.KisiId && k.Durum == DurumEnum.Aktif).ToList();
            bool result1 = false, result2 = false;
            if (kullanici != null && kullaniciRolleri != null)
            {
                result1 = _kullaniciDal.Delete(kullanici) > 0 ? true : false;
                result2 = _kullaniciRolDal.DeleteAll(kullaniciRolleri) > 0 ? true : false;
            }
            if (result1 && result2)
                return true;
            return false;
        }

        public ServiceResult<KullaniciModel> GetKullaniciBilgileri(string kullaniciAd, string kullaniciSifre)
        {
            var admin = new KullaniciModel();
            if (string.Equals(kullaniciAd, admin.Ad, StringComparison.OrdinalIgnoreCase) && string.Equals(kullaniciSifre, admin.Sifre))
                return new ServiceResult<KullaniciModel>(admin);

            var kullanici = new ServiceResult<KullaniciModel>(_kullaniciDal.GetKullaniciBilgileri(kullaniciAd, kullaniciSifre));
            if (kullanici.Result == null)
            {
                return new ServiceResult<KullaniciModel>(kullanici.Result, "Yanlış kullanıcı adı veya şifre", ServiceResultType.Unauthorized);

            }
            return kullanici;
        }

        public bool IsKullanici(string kullaniciAd)
        {
            if(_kullaniciDal.Any(k=>k.Ad == kullaniciAd && k.Id != CurrentUser.Id))
            {
                throw new Exception("Kullanıcı adı kullanılıyor!");
            }
            return false;
        }

        public bool IsUsedSifre(string sifre)
        {
            if (_kullaniciDal.Any(k => k.Sifre == sifre))
            {
                return true;
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
            return _kullaniciDal.Get(k => k.Id == kisiId && k.Durum == DurumEnum.Aktif);
        }
    }
}
