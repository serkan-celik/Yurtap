using System.Collections.Generic;
using System.Linq;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Abstract
{
    public interface IKullaniciRolBll
    {
        List<KullaniciRolListeModel> GetKullaniciRolleriListesi();
        KullaniciRolEntity AddKullaniciRol(KullaniciRolEntity kullaniciRolEntity);
        List<KullaniciRolModel> GetKullaniciRolleriById(int kisiId);
        bool IsKullaniciRol(int kisiId, int rolId);
        bool DeleteKullaniciRol(KullaniciRolEntity kullaniciRolEntity);
        KullaniciRolEntity UpdateKullaniciRol(KullaniciRolEntity kullaniciRolEntity);
    }
}
