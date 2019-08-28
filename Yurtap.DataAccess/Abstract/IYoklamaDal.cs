using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Core.DataAccess;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.DataAccess.Abstract
{
    public interface IYoklamaDal : IEntityRepository<YoklamaEntity>
    {
        List<YoklamaListeModel> GetYoklamaListesi();
        List<YoklamaModel> GetYoklamaListeleriByTarih(DateTime tarih);
        YoklamaModel GetYoklamaDetayById(int id);
    }
}
