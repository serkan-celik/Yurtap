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
using Yurtap.Model.ReportModels.YoklamaModels;
using Yurtap.Core.Utilities.ExtensionMethods;
using Microsoft.EntityFrameworkCore;

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

        public List<YoklamaAylikKatilimModel> GetYoklamaKatilimDurumuAylikRaporListesi(DateTime tarih, byte yoklamaBaslikId)
        {
            List<YoklamaAylikKatilimModel> items = new List<YoklamaAylikKatilimModel>();

            using (var context = new YurtapDbContext())
            {
                var yoklamaListeleri = context.Yoklamalar
                                       .Where(yoklamalar => tarih.Month == yoklamalar.Tarih.Month && tarih.Year == yoklamalar.Tarih.Year && yoklamalar.YoklamaBaslikId == yoklamaBaslikId).ToList()
                                        .Select(y =>
                                        JsonConvert.DeserializeObject<IEnumerable<YoklamaListeModel>>(y.Liste)
                                        .Select(a =>
                                            new YoklamaAylikKatilimModel
                                            {
                                                AdSoyad = String.Join(' ', a.Ad, a.Soyad),
                                                Gun = y.Tarih.Day,
                                                Katilim = a.YoklamaDurum.GetDescription()
                                            }
                                        )).ToList();

                yoklamaListeleri.ForEach(a => items.AddRange(a.ToList()));


                items = items.GroupBy(y => y.AdSoyad).Select(y =>
                 new YoklamaAylikKatilimModel
                 {
                     AdSoyad = y.Key,
                     Gun1 = y.SingleOrDefault(g => g.Gun == 1)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 1)?.Katilim : "",
                     Gun2 = y.SingleOrDefault(g => g.Gun == 2)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 2)?.Katilim : "",
                     Gun3 = y.SingleOrDefault(g => g.Gun == 3)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 3)?.Katilim : "",
                     Gun4 = y.SingleOrDefault(g => g.Gun == 4)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 4)?.Katilim : "",
                     Gun5 = y.SingleOrDefault(g => g.Gun == 5)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 5)?.Katilim : "",
                     Gun6 = y.SingleOrDefault(g => g.Gun == 6)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 6)?.Katilim : "",
                     Gun7 = y.SingleOrDefault(g => g.Gun == 7)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 7)?.Katilim : "",
                     Gun8 = y.SingleOrDefault(g => g.Gun == 8)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 8)?.Katilim :"",
                     Gun9 = y.SingleOrDefault(g => g.Gun == 9)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 9)?.Katilim : "",
                     Gun10 = y.SingleOrDefault(g => g.Gun == 10)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 10)?.Katilim : "",
                     Gun11 = y.SingleOrDefault(g => g.Gun == 11)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 11)?.Katilim : "",
                     Gun12 = y.SingleOrDefault(g => g.Gun == 12)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 12)?.Katilim : "",
                     Gun13 = y.SingleOrDefault(g => g.Gun == 13)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 13)?.Katilim : "",
                     Gun14 = y.SingleOrDefault(g => g.Gun == 14)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 14)?.Katilim : "",
                     Gun15 = y.SingleOrDefault(g => g.Gun == 15)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 15)?.Katilim : "",
                     Gun16 = y.SingleOrDefault(g => g.Gun == 16)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 16)?.Katilim : "",
                     Gun17 = y.SingleOrDefault(g => g.Gun == 17)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 17)?.Katilim : "",
                     Gun18 = y.SingleOrDefault(g => g.Gun == 18)?.Gun !=0 ? y.SingleOrDefault(g => g.Gun == 18)?.Katilim : "",
                     Gun19 = y.SingleOrDefault(g => g.Gun == 19)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 19)?.Katilim : "",
                     Gun20 = y.SingleOrDefault(g => g.Gun == 20)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 20)?.Katilim : "",
                     Gun21 = y.SingleOrDefault(g => g.Gun == 21)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 21)?.Katilim : "",
                     Gun22 = y.SingleOrDefault(g => g.Gun == 22)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 22)?.Katilim : "",
                     Gun23 = y.SingleOrDefault(g => g.Gun == 23)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 23)?.Katilim : "",
                     Gun24 = y.SingleOrDefault(g => g.Gun == 24)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 24)?.Katilim : "",
                     Gun25 = y.SingleOrDefault(g => g.Gun == 25)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 25)?.Katilim : "",
                     Gun26 = y.SingleOrDefault(g => g.Gun == 26)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 26)?.Katilim : "",
                     Gun27 = y.SingleOrDefault(g => g.Gun == 27)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 27)?.Katilim : "",
                     Gun28 = y.SingleOrDefault(g => g.Gun == 28)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 28)?.Katilim : "",
                     Gun29 = y.SingleOrDefault(g => g.Gun == 29)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 29)?.Katilim : "",
                     Gun30 = y.SingleOrDefault(g => g.Gun == 30)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 30)?.Katilim : "",
                     Gun31 = y.SingleOrDefault(g => g.Gun == 31)?.Gun != 0 ? y.SingleOrDefault(g => g.Gun == 31)?.Katilim : "",
                 }
                ).ToList();
                return items.ToList();
            }
        }

        public List<YoklamaAylikYuzdelikKatilimModel> GetYoklamaKatilimYuzdesiAylikRaporListesi(DateTime tarih)
        {
            List<YoklamaAylikYuzdelikKatilimModel> items = new List<YoklamaAylikYuzdelikKatilimModel>();

            using (var context = new YurtapDbContext())
            {
                var yoklamaListeleri = context.Yoklamalar
                                       .Where(yoklamalar => tarih.Month == yoklamalar.Tarih.Month && tarih.Year == yoklamalar.Tarih.Year).ToList()
                                        .Select(y =>
                                        JsonConvert.DeserializeObject<IEnumerable<YoklamaListeModel>>(y.Liste)
                                        .Select(a =>
                                            new YoklamaAylikYuzdelikKatilimModel
                                            {
                                                AdSoyad = String.Join(' ', a.Ad, a.Soyad),
                                                YoklamaBaslik = y.YoklamaBaslikId,
                                                Katilim = a.YoklamaDurum.GetDisplayName()
                                            }
                                        )).ToList();
                yoklamaListeleri.ForEach(a => items.AddRange(a.ToList()));
                items = items.GroupBy(y => new { y.AdSoyad }).Select(y =>
                    new YoklamaAylikYuzdelikKatilimModel
                    {
                        AdSoyad = y.Key.AdSoyad,
                        YoklamaIstatistikleri = y.GroupBy(g => new { g.YoklamaBaslik }).Select(b => new YoklamaIstatistik
                        {
                            YoklamaBaslik = b.Key.YoklamaBaslik,
                            KatilimYuzdesi = Math.Floor(
                           (Convert.ToDouble(b.Count(c => c.Katilim == YoklamaDurumEnum.Var.GetDisplayName() && c.YoklamaBaslik == b.Key.YoklamaBaslik)) / Convert.ToDouble(b.Count(c => c.YoklamaBaslik == b.Key.YoklamaBaslik))) * 100).ToString()
                        }).ToList(),
                        YoklamaSayisi = y.Count(),
                        KatilimSayisi = y.Count(c => c.Katilim == YoklamaDurumEnum.Var.GetDisplayName()),
                        GenelKatilimYuzdesi = Math.Floor(
                        (Convert.ToDouble(y.Count(c => c.Katilim == YoklamaDurumEnum.Var.GetDisplayName())) / Convert.ToDouble(y.Count())) * 100).ToString()
                    }).ToList();
                return items;
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
