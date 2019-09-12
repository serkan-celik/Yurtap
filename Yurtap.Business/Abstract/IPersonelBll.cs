using System.Collections.Generic;
using System.Linq;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IPersonelBll
    {
        PersonelModel AddPersonel(PersonelModel personelModel);
        List<PersonelModel> GetPersonelListesi();
        PersonelModel GetPersonelByKisiId(int kisiId);
        PersonelEntity GetPersonel(int kisiId);
        bool IsPersonelMi(PersonelModel personelModel);
        PersonelModel UpdatePersonel(PersonelModel personelModel);
        bool DeletePersonel(PersonelModel personelModel);
    }
}
