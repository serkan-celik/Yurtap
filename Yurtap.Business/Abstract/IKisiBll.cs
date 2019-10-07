using System.Collections.Generic;
using System.Linq;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IKisiBll
    {
        ServiceResult<KisiEntity> GetKisi(int kisiId);
        OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel);
        PersonelModel UpdatePersonel(PersonelModel personelModel);
    }
}
