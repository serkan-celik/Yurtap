
using System.Collections.Generic;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using System.Linq;
using Yurtap.Entity.Models;
using Yurtap.Core.Entity.Enums;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfOgrenciDal : EfEntityRepositoryBase<OgrenciEntity, YurtapDbContext>, IOgrenciDal
    {
        public OgrenciModel GetOgrenci(short kisiId)
        {
            return GetOgrenciListesi().SingleOrDefault(o => o.KisiId == kisiId);
        }

        public List<OgrenciModel> GetOgrenciListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var ogrenciler = from ogrenci in context.Ogrenciler
                                 join kisi in context.Kisiler on ogrenci.Id equals kisi.Id
                                 join kullanici in context.Kullanicilar on kisi.Id equals kullanici.Id into u 
                                 from kullanici in u.DefaultIfEmpty()
                                 orderby kisi.Ad, kisi.Soyad ascending 
                                 where ogrenci.Durum == DurumEnum.Aktif && kisi.Durum == DurumEnum.Aktif
                                 select new OgrenciModel
                                 {
                                     KisiId = kisi.Id,
                                     Ad = kisi.Ad,
                                     Soyad = kisi.Soyad,
                                     TcKimlikNo = kisi.TcKimlikNo,
                                     KullaniciAd =kullanici.Ad,
                                     Sifre = kullanici.Sifre
                                 };
                return ogrenciler.ToList();
            }
        }
    }
}
