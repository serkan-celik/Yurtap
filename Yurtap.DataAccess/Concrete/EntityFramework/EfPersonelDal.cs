using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using System.Linq;
using Yurtap.Entity.Models;
using Yurtap.Core.Entity.Enums;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfPersonelDal : EfEntityRepositoryBase<PersonelEntity, YurtapDbContext>, IPersonelDal
    {
        public PersonelModel GetPersonel(short kisiId)
        {
            return GetPersonelListesi().SingleOrDefault(o => o.KisiId == kisiId);
        }

        public List<PersonelModel> GetPersonelListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var personeller = from personel in context.Personeller
                                  join kisi in context.Kisiler on personel.Id equals kisi.Id
                                  join kullanici in context.Kullanicilar on kisi.Id equals kullanici.Id into u
                                  from kullanici in u.DefaultIfEmpty()
                                  orderby kisi.Ad, kisi.Soyad ascending
                                  where personel.Durum == DurumEnum.Aktif && kisi.Durum == DurumEnum.Aktif
                                  select new PersonelModel
                                  {
                                      KisiId = kisi.Id,
                                      Ad = kisi.Ad,
                                      Soyad = kisi.Soyad,
                                      TcKimlikNo = kisi.TcKimlikNo,
                                      KullaniciAd = kullanici.Ad,
                                      Sifre = kullanici.Sifre
                                  };
                return personeller.ToList();
            }
        }

        public bool IsPersonelMi(PersonelModel personelModel)
        {
            using (var context = new YurtapDbContext())
            {
                var personelMi = from personel in context.Personeller
                                 join kisi in context.Kisiler on personel.Id equals kisi.Id
                                 where personel.Id != personelModel.KisiId && kisi.Ad == personelModel.Ad && kisi.Soyad == personelModel.Soyad && personel.Durum == DurumEnum.Aktif
                                 select personel;
                return personelMi.Any();
            }
        }
    }
}
