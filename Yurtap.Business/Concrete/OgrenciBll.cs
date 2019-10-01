using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Transactions;
using Yurtap.Business.Abstract;
using Yurtap.Core.DataAccess;
using Yurtap.Core.Entity.Enums;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Enums;
using Yurtap.Entity.Models;
using Yurtap.Core.Utilities.ExtensionMethods;

namespace Yurtap.Business.Concrete
{
    public class OgrenciBll : BaseManager, IOgrenciBll
    {
        private readonly IOgrenciDal _ogrenciDal;
        private readonly IKisiDal _kisiDal;
        private readonly IKisiBll _kisiBll;
        private readonly IKullaniciBll _kullaniciBll;
        private readonly IKullaniciRolBll _kullaniciRolBll;

        public OgrenciBll(IOgrenciDal ogrenciDal, IKisiDal kisiDal, IKisiBll kisiBll, IKullaniciBll kullaniciBll, IKullaniciRolBll kullaniciRolBll)
        {
            _ogrenciDal = ogrenciDal;
            _kisiDal = kisiDal;
            _kisiBll = kisiBll;
            _kullaniciBll = kullaniciBll;
            _kullaniciRolBll = kullaniciRolBll;
        }

        public OgrenciModel AddOgrenci(OgrenciModel ogrenciModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IsOgrenciMi(ogrenciModel);
                var kisi = _kisiDal.Add(
                    new KisiEntity
                    {
                        Ad = ogrenciModel.Ad,
                        Soyad = ogrenciModel.Soyad
                        //TcKimlikNo = ogrenciModel.TcKimlikNo,
                    });
                var ogrenci = _ogrenciDal.Add(new OgrenciEntity
                {
                    Id = kisi.Id,
                });
                ogrenciModel.KisiId = ogrenci.Id;
                if (ogrenciModel.Hesap)
                {
                    string ad = ogrenciModel.Ad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                    string soyad = ogrenciModel.Soyad.ToLower().ConvertTRCharToENChar();
                    string defaultKullaniciAd = String.Join(".", ad, soyad);
                    string defaultSifre = new Random().Next(1234, 9999).ToString();

                    _kullaniciBll.AddKullanici(new KullaniciModel()
                    {
                        KisiId = kisi.Id,
                        Ad = defaultKullaniciAd,
                        Sifre = defaultSifre
                    });
                    _kullaniciRolBll.AddKullaniciRol(new KullaniciRolEntity
                    {
                        KisiId = kisi.Id,
                        RolId = RolEnum.GenelYonetici.GetHashCode()
                    });

                }
                scope.Complete();
            }
            return ogrenciModel;
        }

        public List<OgrenciModel> GetOgrenciListesi()
        {
            return _ogrenciDal.GetOgrenciListesi();
        }

        public OgrenciModel GetOgrenciByKisiId(int kisiId)
        {
            bool kullaniciMi = _kullaniciBll.IsKullanici(kisiId);
            OgrenciModel ogrenci = GetOgrenciListesi().SingleOrDefault(o => o.KisiId == kisiId);
            if (kullaniciMi)
                ogrenci.Hesap = true;
            else
                ogrenci.Hesap = false;
            return ogrenci;
        }

        public OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel)
        {
            IsOgrenciMi(ogrenciModel);
            var kullanici = _kullaniciBll.GetKullaniciById(ogrenciModel.KisiId);
            if (kullanici == null && ogrenciModel.Hesap)
            {
                string ad = ogrenciModel.Ad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                string soyad = ogrenciModel.Soyad.ToLower().ConvertTRCharToENChar();
                string defaultKullaniciAd = String.Join(".", ad, soyad);
                string defaultSifre = new Random().Next(1234, 9999).ToString();          

                _kullaniciBll.AddKullanici(new KullaniciModel()
                {
                    KisiId = ogrenciModel.KisiId,
                    Ad = defaultKullaniciAd,
                    Sifre = defaultSifre,
                });
            }
            else if (kullanici != null)
            {
                if (ogrenciModel.Hesap)
                    kullanici.Durum = DurumEnum.Aktif;
                else
                    kullanici.Durum = DurumEnum.Pasif;
                _kullaniciBll.UpdateKullanici(kullanici);
            }
            var ogrenci = GetOgrenci(ogrenciModel.KisiId);
            ogrenci.SonGuncelleyenId = CurrentUser.Id;
            ogrenci.SonGuncellemeTarihi = DateTime.Now;
            _ogrenciDal.Update(ogrenci);
            return _kisiBll.UpdateOgrenci(ogrenciModel);
        }

        public bool DeleteOgrenci(OgrenciModel ogrenciModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var kisi = _kisiBll.GetKisi(ogrenciModel.KisiId);
                kisi.Durum = DurumEnum.Silinmis;
                _kisiDal.Update(kisi);
                var ogrenci = GetOgrenci(ogrenciModel.KisiId);
                ogrenci.Durum = DurumEnum.Silinmis;
                _ogrenciDal.Update(ogrenci);
                scope.Complete();
            }
            return true;
        }

        public bool IsOgrenciMi(OgrenciModel ogrenciModel)
        {
            bool isOgrenci = _ogrenciDal.IsOgrenciMi(ogrenciModel);
            bool isKisi = _kisiDal.Any(k => k.TcKimlikNo == ogrenciModel.TcKimlikNo);
            if (isOgrenci)
            {
                throw new Exception("Öğrenci daha önceden kayıtlıdır!");
            }
            if (isKisi && ogrenciModel.KisiId == 0)
            {
                throw new Exception("Tc kimlik no daha önceden tanımlıdır!");
            }
            return true;
        }

        public OgrenciEntity GetOgrenci(int kisiId)
        {
            return _ogrenciDal.Get(o => o.Id == kisiId);
        }
    }
}
