
using System.Collections.Generic;
using Yurtap.Core.DataAccess;
using Yurtap.Core.Entity;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.DataAccess.Abstract
{
    public interface IOgrenciDal : IEntityRepository<OgrenciEntity>
    {
        List<OgrenciModel> GetOgrenciListesi();
        OgrenciModel GetOgrenci(short kisiId);
        bool IsOgrenciMi(OgrenciModel ogrenciModel);
    }
}
