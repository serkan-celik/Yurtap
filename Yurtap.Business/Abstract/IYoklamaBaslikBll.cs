using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IYoklamaBaslikBll
    {
        ServiceResult<List<YoklamaBaslikModel>> GetYoklamaBaslikListesi();
        ServiceResult<YoklamaBaslikEntity> AddYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        ServiceResult<YoklamaBaslikEntity> GetYoklamaBaslik(byte yoklamaBaslikId);
        ServiceResult<YoklamaBaslikEntity> IsYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        ServiceResult<YoklamaBaslikEntity> UpdateYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        ServiceResult<bool> DeleteYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);

    }
}
