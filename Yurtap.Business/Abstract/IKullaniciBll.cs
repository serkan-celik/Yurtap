using System.Collections.Generic;
using System.Linq;
using Yurtap.Core.Business.Models;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IKullaniciBll
    {

        KullaniciEntity AddGenerateKullanici(KullaniciRolEntity kullaniciRolEntity);
        KullaniciEntity GetKullaniciById(int kisiId);
        ServiceResult<KullaniciModel> GetKullaniciBilgileri(string kullaniciAdi, string kullaniciSifre);
        KullaniciEntity UpdateKullanici(KullaniciModel kullaniciModel);
        KullaniciEntity UpdateKullanici(KullaniciEntity kullaniciEntity);
        bool DeleteKullanici(KullaniciRolListeModel kullaniciModel);
        bool IsKullanici(int kisiId);
        bool IsKullanici(string kullaniciAd);
    }
}
