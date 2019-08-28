using System.Collections.Generic;
using System.Linq;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IKisiBll
    {
        KisiEntity GetKisi(int kisiId);
        OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel);
        PersonelModel UpdatePersonel(PersonelModel personelModel);
    }
}
