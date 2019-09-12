using System.Collections.Generic;
using Yurtap.Core.DataAccess;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.DataAccess.Abstract
{
    public interface IPersonelDal : IEntityRepository<PersonelEntity>
    {
        List<PersonelModel> GetPersonelListesi();
        PersonelModel GetPersonel(short kisiId);
        bool IsPersonelMi(PersonelModel personelModel);
    }
}
