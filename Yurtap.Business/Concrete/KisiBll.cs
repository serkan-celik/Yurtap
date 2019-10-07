using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Business.Models;
using Yurtap.Core.Entity.Enums;
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

        public ServiceResult<KisiEntity> GetKisi(int kisiId)
        {
            var kisi = _kisiDal.Get(k => k.Id == kisiId && k.Durum == DurumEnum.Aktif);
            if (kisi == null)
            {
                return new ServiceResult<KisiEntity>(null, "Kişi bulunamadı", ServiceResultType.NotFound);
            }
            return new ServiceResult<KisiEntity>(kisi);
        }

        public OgrenciModel UpdateOgrenci(OgrenciModel ogrenciModel)
        {
            var kisi = GetKisi(ogrenciModel.KisiId);
            if(kisi.Success)
            kisi.Result.Ad = ogrenciModel.Ad;
            kisi.Result.Soyad = ogrenciModel.Soyad;
            kisi.Result.SonGuncelleyenId = CurrentUser.Id;
            kisi.Result.SonGuncellemeTarihi = DateTime.Now;
            _kisiDal.Update(kisi.Result);
            return ogrenciModel;
        }

        public PersonelModel UpdatePersonel(PersonelModel personelModel)
        {
            var kisi = GetKisi(personelModel.KisiId);
            if (kisi.Success)
            kisi.Result.Ad = personelModel.Ad;
            kisi.Result.Soyad = personelModel.Soyad;
            kisi.Result.SonGuncelleyenId = CurrentUser.Id;
            kisi.Result.SonGuncellemeTarihi = DateTime.Now;
            _kisiDal.Update(kisi.Result);
            return personelModel;
        }
    }
}
