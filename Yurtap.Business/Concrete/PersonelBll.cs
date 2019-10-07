using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Yurtap.Business.Abstract;
using Yurtap.Core.Entity.Enums;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Enums;
using Yurtap.Entity.Models;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.Core.Business.Models;
using Microsoft.AspNetCore.Http;

namespace Yurtap.Business.Concrete
{
    public class PersonelBll : BaseManager, IPersonelBll
    {
        private readonly IPersonelDal _personelDal;
        private readonly IKisiDal _kisiDal;
        private readonly IKisiBll _kisiBll;
        private readonly IKullaniciBll _kullaniciBll;
        private readonly IKullaniciRolBll _kullaniciRolBll;

        public PersonelBll(IPersonelDal personelDal, IKisiDal kisiDal, IKisiBll kisiBll, IKullaniciBll kullaniciBll, IKullaniciRolBll kullaniciRolBll)
        {
            _personelDal = personelDal;
            _kisiDal = kisiDal;
            _kisiBll = kisiBll;
            _kullaniciBll = kullaniciBll;
            _kullaniciRolBll = kullaniciRolBll;
        }

        public ServiceResult<object> AddPersonel(PersonelModel personelModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (!IsPersonelMi(personelModel).Success)
                {
                    return IsPersonelMi(personelModel);
                }
                var kisi = _kisiDal.Add(
                new KisiEntity
                {
                    Ad = personelModel.Ad,
                    Soyad = personelModel.Soyad
                        //TcKimlikNo = personelModel.TcKimlikNo
                    });
                var personel = _personelDal.Add(new PersonelEntity
                {
                    Id = kisi.Id
                });
                personelModel.KisiId = personel.Id;
                if (personelModel.Hesap)
                {
                    string ad = personelModel.Ad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                    string soyad = personelModel.Soyad.ToLower().ConvertTRCharToENChar();
                    string defaultKullaniciAd = String.Join(".", ad, soyad);
                    string defaultSifre = new Random().Next(1234, 9999).ToString();

                    _kullaniciBll.AddKullanici(new KullaniciModel()
                    {
                        KisiId = kisi.Id,
                        Ad = defaultKullaniciAd,
                        Sifre = defaultSifre,
                    });
                    _kullaniciRolBll.AddKullaniciRol(new KullaniciRolEntity
                    {
                        KisiId = kisi.Id,
                        RolId = RolEnum.GenelYonetici.GetHashCode()
                    });
                }
                scope.Complete();
                return new ServiceResult<object>(personelModel, "Personel oluşturuldu", ServiceResultType.Created);
            }
        }

        public ServiceResult<List<PersonelModel>> GetPersonelListesi()
        {
            var personelListesi = new ServiceResult<List<PersonelModel>>(_personelDal.GetPersonelListesi());
            if (!personelListesi.Result.Any())
            {
                return new ServiceResult<List<PersonelModel>>(personelListesi.Result, "Personel bulunamadı", ServiceResultType.NotFound);
            }
            return personelListesi;
        }

        public ServiceResult<PersonelModel> GetPersonelByKisiId(int kisiId)
        {
            bool kullaniciMi = _kullaniciBll.IsKullanici(kisiId);
            var personel = _personelDal.GetPersonel(kisiId);
            if (personel == null)
            {
                return new ServiceResult<PersonelModel>(personel, "Personel bulunamadı", ServiceResultType.NotFound);
            }
            if (kullaniciMi)
                personel.Hesap = true;
            return new ServiceResult<PersonelModel>(personel);
        }

        public ServiceResult<PersonelEntity> GetPersonel(int kisiId)
        {
            var personel = _personelDal.Get(p => p.Id == kisiId && p.Durum == DurumEnum.Aktif);
            if (personel == null)
            {
                return new ServiceResult<PersonelEntity>(null, "Personel bulunamadı", ServiceResultType.NotFound);
            }
            return new ServiceResult<PersonelEntity>(personel);
        }

        public ServiceResult<object> UpdatePersonel(PersonelModel personelModel)
        {
            if (!IsPersonelMi(personelModel).Success)
            {
                return IsPersonelMi(personelModel);
            }
            var kullanici = _kullaniciBll.GetKullaniciById(personelModel.KisiId);
            if (kullanici == null && personelModel.Hesap)
            {
                string ad = personelModel.Ad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                string soyad = personelModel.Soyad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                string defaultKullaniciAd = String.Join(".", ad, soyad);
                string defaultSifre = new Random().Next(1234, 9999).ToString();

                _kullaniciBll.AddKullanici(new KullaniciModel()
                {
                    KisiId = personelModel.KisiId,
                    Ad = defaultKullaniciAd,
                    Sifre = defaultSifre,
                });
            }
            else if (kullanici != null)
            {
                if (personelModel.Hesap)
                    kullanici.Durum = DurumEnum.Aktif;
                else
                    kullanici.Durum = DurumEnum.Pasif;
                _kullaniciBll.UpdateKullanici(kullanici);
            }
            var personel = GetPersonel(personelModel.KisiId);
            if (!personel.Success)
            {
                return new ServiceResult<object>(personel.Result, personel.Message, personel.ResultCode);
            }
            personel.Result.SonGuncelleyenId = CurrentUser.Id;
            personel.Result.SonGuncellemeTarihi = DateTime.Now;
            _personelDal.Update(personel.Result);

            var updatedPersonel = _kisiBll.UpdatePersonel(personelModel);
            return new ServiceResult<object>(updatedPersonel, "Personel güncelledi", ServiceResultType.Success);
        }
        public ServiceResult<object> DeletePersonel(PersonelModel personelModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var kisi = _kisiBll.GetKisi(personelModel.KisiId);
                if (!kisi.Success)
                {
                    return new ServiceResult<object>(kisi.Result, kisi.Message,kisi.ResultCode);
                }
                kisi.Result.Durum = DurumEnum.Silinmis;
                _kisiDal.Update(kisi.Result);

                var personel = GetPersonel(personelModel.KisiId);
                if (!personel.Success)
                {
                    return new ServiceResult<object>(personel.Result, personel.Message, personel.ResultCode);
                }
                personel.Result.Durum = DurumEnum.Silinmis;
                _personelDal.Update(personel.Result);
                scope.Complete();
                return new ServiceResult<object>(null, "Personel silindi", ServiceResultType.Success);
            }  
        }

        public ServiceResult<object> IsPersonelMi(PersonelModel personelModel)
        {
            bool personelMi = _personelDal.IsPersonelMi(personelModel);
            //bool isKisi = _kisiDal.Any(k => k.TcKimlikNo == personelModel.TcKimlikNo);
            if (personelMi)
            {
                return new ServiceResult<object>(null, "Personel zaten kayıtlıdır", ServiceResultType.BadRequest);
            }
            //if (isKisi && personelModel.KisiId == 0)
            //{
            //    return true;
            //}
            return new ServiceResult<object>(personelModel);
        }
    }
}
