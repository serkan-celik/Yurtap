using System;
using System.Collections.Generic;
using System.Text;
using Yurtap.Core.DataAccess.EntityFramework;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;

namespace Yurtap.DataAccess.Concrete.EntityFramework
{
    public class EfKullaniciRolDal : EfEntityRepositoryBase<KullaniciRolEntity, YurtapDbContext>, IKullaniciRolDal
    {

    }
}
