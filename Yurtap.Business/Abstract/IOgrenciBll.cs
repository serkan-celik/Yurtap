using System.Collections.Generic;
using System.Linq;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IOgrenciBll
    {
        ServiceResult<object> AddOgrenci(OgrenciModel ogrenciModel);
        ServiceResult<List<OgrenciModel>> GetOgrenciListesi();
        ServiceResult<OgrenciModel> GetOgrenciByKisiId(int kisiId);
        ServiceResult<OgrenciEntity> GetOgrenci(int kisiId);
        ServiceResult<object> IsOgrenciMi(OgrenciModel ogrenciModel);
        ServiceResult<object> UpdateOgrenci(OgrenciModel ogrenciModel);
        ServiceResult<object> DeleteOgrenci(OgrenciModel ogrenciModel);
    }
}