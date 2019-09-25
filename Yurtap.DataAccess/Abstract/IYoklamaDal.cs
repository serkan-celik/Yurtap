using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Core.DataAccess;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Model.ReportModels.YoklamaModels;

namespace Yurtap.DataAccess.Abstract
{
    public interface IYoklamaDal : IEntityRepository<YoklamaEntity>
    {
        List<YoklamaListeModel> GetYoklamaListesi();
        List<YoklamaModel> GetYoklamaListeleriByTarih(DateTime tarih);
        YoklamaModel GetYoklamaDetayById(int id);
        List<YoklamaAylikKatilimModel> GetYoklamaKatilimDurumuAylikRaporListesi(DateTime tarih, byte YoklamaBaslikId);
        List<YoklamaAylikYuzdelikKatilimModel> GetYoklamaKatilimYuzdesiAylikRaporListesi(DateTime tarih);
    }
}
