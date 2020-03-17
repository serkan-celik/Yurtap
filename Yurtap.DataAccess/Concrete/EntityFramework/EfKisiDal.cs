using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Core.Entity.Enums;
using Yurtap.Core.Enums;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfKisiDal : EfEntityRepositoryBase<KisiEntity, YurtapDbContext>, IKisiDal
    {
        public List<KisiListeModel> GetKisiListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var kisiListesi = (from k in context.Kisiler
                                   join p in context.Personeller on k.Id equals p.Id into p1
                                   from p in p1.DefaultIfEmpty()
                                   join o in context.Ogrenciler on k.Id equals o.Id into o1
                                   from o in o1.DefaultIfEmpty()
                                   where
                                   k.Durum == DurumEnum.Aktif
                                   && (p.Durum == DurumEnum.Aktif || o.Durum == DurumEnum.Aktif)
                                   orderby k.Ad, k.Soyad ascending
                                   select new KisiListeModel
                                   {
                                       KisiTip = k.Id == p.Id ? KisiEnum.Personel : KisiEnum.Ogrenci,
                                       KisiId = k.Id,
                                       AdSoyad = string.Join(' ', k.Ad, k.Soyad)
                                   }).OrderBy(k=>k.KisiTip).ToList();
                return kisiListesi;
            }
        }
    }
}
