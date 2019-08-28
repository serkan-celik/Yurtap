
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
                var ogrenciler = from o in context.Ogrenciler
                                 join k in context.Kisiler
                                 on o.Id equals k.Id
                                 orderby k.Ad,k.Soyad ascending 
                                 where o.Durum == DurumEnum.Aktif && k.Durum == DurumEnum.Aktif
                                 select new OgrenciModel
                                 {
                                     KisiId = k.Id,
                                     Ad = k.Ad,
                                     Soyad = k.Soyad,
                                     TcKimlikNo = k.TcKimlikNo,
                                 };
                return ogrenciler.ToList();
            }
        }
    }
}
