
using System.Collections.Generic;
using Yurtap.Core.DataAccess;
using Yurtap.Core.Entity;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.DataAccess.Abstract
{
    public interface IKullaniciRolDal : IEntityRepository<KullaniciRolEntity>
    {
        List<KullaniciRolListeModel> GetKullaniciRolleriListesi();
    }
}
