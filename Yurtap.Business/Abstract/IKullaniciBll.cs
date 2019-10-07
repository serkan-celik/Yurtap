using System.Collections.Generic;
using System.Linq;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IKullaniciBll
    {
        List<KullaniciModel> GetKullaniciListesi();
        KullaniciEntity AddKullanici(KullaniciModel kullaniciModel);
        KullaniciEntity GetKullaniciById(int kisiId);
        ServiceResult<KullaniciModel> GetKullaniciBilgileri(string kullaniciAdi, string kullaniciSifre);
        KullaniciEntity UpdateKullanici(KullaniciModel kullaniciModel);
        KullaniciEntity UpdateKullanici(KullaniciEntity kullaniciEntity);
        bool DeleteKullanici(KullaniciEntity kullaniciModel);
        bool IsKullanici(int kisiId);
        bool IsKullanici(string kullaniciAd);
    }
}
