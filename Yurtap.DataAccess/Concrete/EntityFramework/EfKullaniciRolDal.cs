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
using Yurtap.Core.Utilities.ExtensionMethods;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfKullaniciRolDal : EfEntityRepositoryBase<KullaniciRolEntity, YurtapDbContext>, IKullaniciRolDal
    {
        public List<KullaniciRolListeModel> GetKullaniciRolleriListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var kullaniciRolBilgileri = (from kr in context.KullaniciRolleri
                                             join k in context.Kisiler
                                            on kr.KisiId equals k.Id

                                             where
                                              kr.Durum == DurumEnum.Aktif
                                              && k.Durum == DurumEnum.Aktif
                                             group k by k into kisi
                                             select new KullaniciRolListeModel
                                             {
                                                 KisiId = kisi.Key.Id,
                                                 AdSoyad = kisi.Key.Ad + " " + kisi.Key.Soyad,
                                                 KullaniciAd = context.Kullanicilar.SingleOrDefault(k => k.Id == kisi.Key.Id).Ad,
                                                 Sifre = context.Kullanicilar.SingleOrDefault(k => k.Id == kisi.Key.Id).Sifre,
                                                 Roller = context.KullaniciRolleri.Where(kr => kr.KisiId == kisi.Key.Id && kr.Durum == DurumEnum.Aktif).Select(kr => Enum.Parse<RolEnum>(kr.RolId.ToString()).GetDisplayName()).OrderBy(kr => kr).ToArray()
                                             }).ToList();
                return kullaniciRolBilgileri;
            }
        }
    }
}
