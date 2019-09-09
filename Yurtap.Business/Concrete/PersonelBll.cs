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

        public PersonelModel AddPersonel(PersonelModel personelModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                IsPersonel(personelModel);
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
            }
            return personelModel;
        }

        public List<PersonelModel> GetPersonelListesi()
        {
            return _personelDal.GetPersonelListesi();
        }

        public PersonelModel GetPersonelByKisiId(int kisiId)
        {
            bool kullaniciMi = _kullaniciBll.IsKullanici(kisiId);
            var personel = GetPersonelListesi().SingleOrDefault(o => o.KisiId == kisiId);
            if (kullaniciMi)
                personel.Hesap = true;
            return personel;
        }

        public PersonelEntity GetPersonel(int kisiId)
        {
            return _personelDal.Get(o => o.Id == kisiId);
        }

        public PersonelModel UpdatePersonel(PersonelModel personelModel)
        {
            var kullanici = _kullaniciBll.GetKullaniciById(personelModel.KisiId);
            if (kullanici == null && personelModel.Hesap)
            {
                string ad = personelModel.Ad.ToLower().RemoveEmpty().ConvertTRCharToENChar();
                string soyad = personelModel.Soyad.ToLower().ConvertTRCharToENChar();
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
            personel.SonGuncelleyenId = CurrentUser.Id;
            personel.SonGuncellemeTarihi = DateTime.Now;
            _personelDal.Update(personel);
            return _kisiBll.UpdatePersonel(personelModel);
        }

        public bool DeletePersonel(PersonelModel personelModel)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var kisi = _kisiBll.GetKisi(personelModel.KisiId);
                kisi.Durum = DurumEnum.Silinmis;
                _kisiDal.Update(kisi);
                var personel = GetPersonel(personelModel.KisiId);
                personel.Durum = DurumEnum.Silinmis;
                _personelDal.Update(personel);
                scope.Complete();
            }
            return true;
        }

        public bool IsPersonel(PersonelModel personelModel)
        {
            bool isPersonel = _kisiDal.Any(k => k.Ad == personelModel.Ad && k.Soyad == personelModel.Soyad && k.Durum == DurumEnum.Aktif);
            bool isKisi = _kisiDal.Any(k => k.TcKimlikNo == personelModel.TcKimlikNo);
            if (isPersonel)
            {
                throw new Exception("Personel daha önceden kayıtlıdır!");
            }
            if (isKisi && personelModel.KisiId == 0)
            {
                throw new Exception("Tc kimlik no daha önceden tanımlıdır!");
            }
            return true;
        }
    }
}
