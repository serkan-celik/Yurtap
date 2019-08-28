using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using System.Linq;
using Yurtap.Core.Entity.Enums;
using Yurtap.Entity.Enums;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfKullaniciDal : EfEntityRepositoryBase<KullaniciEntity, YurtapDbContext>, IKullaniciDal
    {
        public KullaniciModel GetKullaniciBilgileri(string kullaniciAd, string sifre)
        {
            using (var context = new YurtapDbContext())
            {
                var kullaniciBilgileri = (from kullanicilar in context.Kullanicilar

                                          join kisiler in context.Kisiler
                                         on kullanicilar.Id equals kisiler.Id
                                          where kullanicilar.Ad == kullaniciAd && kullanicilar.Sifre == sifre
                                          && kullanicilar.Durum == DurumEnum.Aktif
                                          && kisiler.Durum == DurumEnum.Aktif
                                          select new KullaniciModel
                                          {
                                              KisiId = kullanicilar.Id,
                                              AdSoyad = kisiler.Ad + " " + kisiler.Soyad,
                                              Ad = kullanicilar.Ad,
                                              Sifre = kullanicilar.Sifre,
                                              Roller = context.KullaniciRolleri.Where(kr => kr.KisiId == kisiler.Id && kr.Durum == DurumEnum.Aktif).Select(kr => new KullaniciRolModel()
                                              {
                                                  Ad = Enum.GetName(typeof(RolEnum),kr.RolId),
                                                  RolId = kr.RolId,
                                                  Ekleme = kr.Ekleme,
                                                  Silme = kr.Silme,
                                                  Guncelleme = kr.Guncelleme,
                                                  Arama = kr.Arama,
                                                  Listeleme = kr.Listeleme
                                              }).ToList()
                                          }).SingleOrDefault();

                return kullaniciBilgileri;
            }
        }

        public IEnumerable<KullaniciModel> GetKullaniciListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var kullaniciListesi = from kullanicilar in context.Kullanicilar
                                       join kisiler in context.Kisiler
                                       on kullanicilar.Id equals kisiler.Id
                                       select new KullaniciModel
                                       {
                                           AdSoyad = kisiler.Ad + " " + kisiler.Soyad,
                                           Ad = kullanicilar.Ad,
                                           Sifre = kullanicilar.Sifre         
                                       };
                return kullaniciListesi;
            }  
        }
    }
}
