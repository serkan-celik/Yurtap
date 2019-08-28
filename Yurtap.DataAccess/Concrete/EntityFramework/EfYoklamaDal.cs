using System.Linq;
using System.Collections.Generic;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Entity.Enums;
using System;
using Newtonsoft.Json;
using Yurtap.Core.Entity.Enums;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfYoklamaDal : EfEntityRepositoryBase<YoklamaEntity, YurtapDbContext>, IYoklamaDal
    {
        public YoklamaModel GetYoklamaDetayById(int id)
        {
            using (var context = new YurtapDbContext())
            {
                var yoklamaListeleri = from y in context.Yoklamalar join yb 
                                       in context.YoklamaBasliklari on 
                                       y.YoklamaBaslikId equals yb.Id
                                       where y.Id == id && y.Durum == DurumEnum.Aktif
                                       select new YoklamaModel()
                                       {
                                           Id = y.Id,
                                           EkleyenId = y.EkleyenId,
                                           YoklamaBaslikId = y.YoklamaBaslikId,
                                           Baslik = yb.Baslik,
                                           Tarih = y.Tarih,
                                           YoklamaListesi = JsonConvert.DeserializeObject<List<YoklamaListeModel>>(y.Liste)
                                       };
                var yoklamaDetay = yoklamaListeleri.SingleOrDefault();
                var orderedList = from y in yoklamaDetay.YoklamaListesi
                                  orderby y.Ad, y.Soyad ascending
                                  select y;
                yoklamaDetay.YoklamaListesi = orderedList.ToList();

                return yoklamaDetay;
            }
        }

        public List<YoklamaModel> GetYoklamaListeleriByTarih(DateTime tarih)
        {
            using (var context = new YurtapDbContext())
            {
                var yoklamaListeleri = from y in context.Yoklamalar
                                       join yb in context.YoklamaBasliklari
                                       on y.YoklamaBaslikId equals yb.Id
                                       where tarih.Hour == 0 ? y.Tarih.Date == tarih.Date : y.Tarih == tarih
                                       orderby y.Tarih ascending
                                       select new YoklamaModel
                                       {
                                           Id = y.Id,
                                           YoklamaBaslikId = y.YoklamaBaslikId,
                                           Baslik = yb.Baslik,
                                           Tarih = y.Tarih,
                                           //YoklamaListesi = JsonConvert.DeserializeObject<List<YoklamaListeModel>>(y.Liste)
                                       };
                return yoklamaListeleri.ToList();
            }
        }

        public List<YoklamaListeModel> GetYoklamaListesi()
        {
            using (var context = new YurtapDbContext())
            {
                var yoklamaListesi = from o in context.Ogrenciler
                                     join k in context.Kisiler
                                     on o.Id equals k.Id
                                     where o.Durum == DurumEnum.Aktif && k.Durum == DurumEnum.Aktif
                                     select new YoklamaListeModel
                                     {
                                         KisiId = k.Id.ToString(),
                                         Ad = k.Ad,
                                         Soyad = k.Soyad,
                                         YoklamaDurum = YoklamaDurumEnum.Var
                                     };
                var orderedYoklamaListesi = from y in yoklamaListesi.ToList()
                                     orderby y.Ad, y.Soyad ascending
                                     select y;
                return orderedYoklamaListesi.ToList();
            }
        }
    }
}
