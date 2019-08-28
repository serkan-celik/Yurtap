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
                var personeller = from o in context.Personeller
                                  join k in context.Kisiler
                                  on o.Id equals k.Id
                                  orderby k.Ad, k.Soyad ascending
                                  where o.Durum == DurumEnum.Aktif && k.Durum == DurumEnum.Aktif
                                  select new PersonelModel
                                  {
                                      KisiId = k.Id,
                                      Ad = k.Ad,
                                      Soyad = k.Soyad,
                                      TcKimlikNo = k.TcKimlikNo
                                  };
                return personeller.ToList();
            }
        }
    }
}
