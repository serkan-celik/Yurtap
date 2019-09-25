using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Model.ReportModels.YoklamaModels;

namespace Yurtap.Business.Abstract
{
    public interface IYoklamaBll
    {
        YoklamaModel AddYoklama(YoklamaModel yoklamaModel);
        List<YoklamaModel> GetYoklamaListeleri(DateTime tarih);
        List<YoklamaListeModel> GetYoklamaListesi();
        YoklamaEntity UpdateYoklama(YoklamaModel yoklamaModel);
        YoklamaModel GetYoklamaDetayById(int id);
        void ExportToExcel(YoklamaModel yoklamaModel);
        List<YoklamaAylikKatilimModel> GetYoklamaKatilimDurumuAylikRaporListesi(DateTime tarih, byte YoklamaBaslikId);
        List<YoklamaAylikYuzdelikKatilimModel> GetYoklamaYuzdelikKatilimAylikRaporListesi(DateTime tarih);
    }
}
