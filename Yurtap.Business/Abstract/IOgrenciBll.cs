using System.Collections.Generic;
using System.Linq;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IOgrenciBll
    {
        OgrenciModel AddOgrenci(OgrenciModel ogrenciModel);
        List<OgrenciModel> GetOgrenciListesi();
        OgrenciEntity GetOgrenci(int kisiId);
        OgrenciModel GetOgrenciByKisiId(int kisiId);
        bool IsOgrenci(OgrenciModel ogrenciModel);
        OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel);
        bool DeleteOgrenci(OgrenciModel ogrenciModel);
    }
}
