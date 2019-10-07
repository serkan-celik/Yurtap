using System.Collections.Generic;
using System.Linq;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IPersonelBll
    {
        ServiceResult<object> AddPersonel(PersonelModel personelModel);
        ServiceResult<List<PersonelModel>> GetPersonelListesi();
        ServiceResult<PersonelModel> GetPersonelByKisiId(int kisiId);
        ServiceResult<PersonelEntity> GetPersonel(int kisiId);
        ServiceResult<object> IsPersonelMi(PersonelModel personelModel);
        ServiceResult<object> UpdatePersonel(PersonelModel personelModel);
        ServiceResult<object> DeletePersonel(PersonelModel personelModel);
    }
}
