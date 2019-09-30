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
        List<YoklamaModel> GetYoklamaListeleri(DateTime? tarih);
        List<YoklamaListeModel> GetYoklamaListesi();
        YoklamaEntity UpdateYoklama(YoklamaModel yoklamaModel);
        YoklamaModel GetYoklamaDetayById(int id);
        byte[] ExportToExcelVakitlikYoklamaRaporu(YoklamaModel yoklama);
        byte[] ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId, string yoklamaBaslik);
        byte[] ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih);
    }
}
