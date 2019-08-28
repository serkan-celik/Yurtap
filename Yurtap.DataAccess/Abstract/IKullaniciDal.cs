
using System.Collections.Generic;
using Yurtap.Core.DataAccess;
using Yurtap.Core.Entity;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.DataAccess.Abstract
{
    public interface IKullaniciDal : IEntityRepository<KullaniciEntity>
    {
        IEnumerable<KullaniciModel> GetKullaniciListesi();
        KullaniciModel GetKullaniciBilgileri(string kullaniciAd, string sifre);
    }
}
