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
using Yurtap.Core.Business.Models;

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

        public ServiceResult<object> AddOgrenci(OgrenciModel ogrenciModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (!IsOgrenciMi(ogrenciModel).Success)
                {
                    return IsOgrenciMi(ogrenciModel);
                }
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
                return new ServiceResult<object>(ogrenciModel, "Öğrenci oluşturuldu", ServiceResultType.Created);
            }
        }

        public ServiceResult<List<OgrenciModel>> GetOgrenciListesi()
        {
            var ogrenciListesi = new ServiceResult<List<OgrenciModel>>(_ogrenciDal.GetOgrenciListesi());
            if (!ogrenciListesi.Result.Any())
            {
                return new ServiceResult<List<OgrenciModel>>(ogrenciListesi.Result, "Öğrenci bulunamadı", ServiceResultType.NotFound);
            }
            return ogrenciListesi;
        }

        public ServiceResult<OgrenciModel> GetOgrenciByKisiId(int kisiId)
        {
            bool kullaniciMi = _kullaniciBll.IsKullanici(kisiId);
            OgrenciModel ogrenci = _ogrenciDal.GetOgrenci(kisiId);
            if (ogrenci == null)
            {
                return new ServiceResult<OgrenciModel>(ogrenci, "Öğrenci bulunamadı", ServiceResultType.NotFound);
            }
            if (kullaniciMi)
                ogrenci.Hesap = true;
            else
                ogrenci.Hesap = false;
            return new ServiceResult<OgrenciModel>(ogrenci);
        }

        public ServiceResult<object> UpdateOgrenci(OgrenciModel ogrenciModel)
        {
            if (!IsOgrenciMi(ogrenciModel).Success)
            {
                return IsOgrenciMi(ogrenciModel);
            }
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
            if (!ogrenci.Success)
            {
                return new ServiceResult<object>(ogrenci.Result, ogrenci.Message, ogrenci.ResultCode);
            }
            ogrenci.Result.SonGuncelleyenId = CurrentUser.Id;
            ogrenci.Result.SonGuncellemeTarihi = DateTime.Now;
            _ogrenciDal.Update(ogrenci.Result);
            var updatedOgrenci = _kisiBll.UpdateOgrenci(ogrenciModel);
            return new ServiceResult<object>(updatedOgrenci, "Öğrenci güncelledi", ServiceResultType.Success);
        }

        public ServiceResult<object> DeleteOgrenci(OgrenciModel ogrenciModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var kisi = _kisiBll.GetKisi(ogrenciModel.KisiId);
                if (!kisi.Success)
                {
                    return new ServiceResult<object>(kisi.Result, kisi.Message, kisi.ResultCode);
                }
                kisi.Result.Durum = DurumEnum.Silinmis;
                _kisiDal.Update(kisi.Result);
                var ogrenci = GetOgrenci(ogrenciModel.KisiId);
                if (!ogrenci.Success)
                {
                    return new ServiceResult<object>(ogrenci.Result, ogrenci.Message, ogrenci.ResultCode);
                }
                ogrenci.Result.Durum = DurumEnum.Silinmis;
                _ogrenciDal.Update(ogrenci.Result);
                scope.Complete();
                return new ServiceResult<object>(null, "Öğrenci silindi", ServiceResultType.Success);
            }
        }

        public ServiceResult<object> IsOgrenciMi(OgrenciModel ogrenciModel)
        {
            bool isOgrenci = _ogrenciDal.IsOgrenciMi(ogrenciModel);
            //bool isKisi = _kisiDal.Any(k => k.TcKimlikNo == ogrenciModel.TcKimlikNo);
            if (isOgrenci)
            {
                return new ServiceResult<object>(null, "Öğrenci zaten kayıtlıdır", ServiceResultType.BadRequest);
            }
            //if (isKisi && ogrenciModel.KisiId == 0)
            //{
            //    throw new Exception("Tc kimlik no daha önceden tanımlıdır!");
            //}
            return new ServiceResult<object>(ogrenciModel);
        }

        public ServiceResult<OgrenciEntity> GetOgrenci(int kisiId)
        {
            var ogrenci = _ogrenciDal.Get(o => o.Id == kisiId && o.Durum == DurumEnum.Aktif);
            if (ogrenci == null)
            {
                return new ServiceResult<OgrenciEntity>(null, "Öğrenci bulunamadı", ServiceResultType.NotFound);
            }
            return new ServiceResult<OgrenciEntity>(ogrenci);
        }
    }
}
