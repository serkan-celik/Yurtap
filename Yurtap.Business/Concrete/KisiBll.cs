using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Concrete
{
    public class KisiBll : BaseManager, IKisiBll
    {
        private readonly IKisiDal _kisiDal;
        public KisiBll(IKisiDal kisiDal)
        {
            _kisiDal = kisiDal;
        }

        public KisiEntity GetKisi(int kisiId)
        {
            return _kisiDal.Get(k => k.Id == kisiId);
        }

        public OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel)
        {
            var kisi = GetKisi(ogrenciModel.KisiId);
            kisi.Ad = ogrenciModel.Ad;
            kisi.Soyad = ogrenciModel.Soyad;
            kisi.SonGuncelleyenId = CurrentUser.Id;
            kisi.SonGuncellemeTarihi = DateTime.Now;
            _kisiDal.Update(kisi);
            return ogrenciModel;
        }

        public PersonelModel UpdatePersonel(PersonelModel personelModel)
        {
            var kisi = GetKisi(personelModel.KisiId);
            kisi.Ad = personelModel.Ad;
            kisi.Soyad = personelModel.Soyad;
            kisi.SonGuncelleyenId = CurrentUser.Id;
            kisi.SonGuncellemeTarihi = DateTime.Now;
            _kisiDal.Update(kisi);
            return personelModel;
        }
    }
}
