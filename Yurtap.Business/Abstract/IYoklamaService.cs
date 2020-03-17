using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Model.ReportModels.YoklamaModels;

namespace Yurtap.Business.Abstract
{
    public interface IYoklamaService
    {
        Task<ServiceResult<object>> AddYoklama(YoklamaModel yoklamaModel);
        ServiceResult<List<YoklamaModel>> GetYoklamaListeleri(DateTime? tarih);
        ServiceResult<List<YoklamaListeModel>> GetYoklamaListesi();
        ServiceResult<object> UpdateYoklama(YoklamaModel yoklamaModel);
        ServiceResult<YoklamaModel> GetYoklamaDetayById(int id);
        ServiceResult<byte[]> ExportToExcelVakitlikYoklamaRaporu(YoklamaModel yoklama);
        ServiceResult<byte[]> ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId, string yoklamaBaslik);
        ServiceResult<byte[]> ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih);
    }
}
