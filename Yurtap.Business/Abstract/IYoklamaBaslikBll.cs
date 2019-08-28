using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IYoklamaBaslikBll
    {
        List<YoklamaBaslikModel> GetYoklamaBaslikListesi();
        YoklamaBaslikEntity AddYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        YoklamaBaslikEntity GetYoklamaBaslik(byte yoklamaBaslikId);
        bool IsYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        YoklamaBaslikEntity UpdateYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
        bool DeleteYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity);
    }
}
