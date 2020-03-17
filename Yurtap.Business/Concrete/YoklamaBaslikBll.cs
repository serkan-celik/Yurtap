using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Business.Models;
using Yurtap.Core.Entity.Enums;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Business.Concrete
{
    public class YoklamaBaslikBll : BaseManager, IYoklamaBaslikBll
    {
        private readonly IYoklamaBaslikDal _yoklamaBaslikDal;
        public YoklamaBaslikBll(IYoklamaBaslikDal yoklamaBaslikDal)
        {
            _yoklamaBaslikDal = yoklamaBaslikDal;
        }

        public ServiceResult<YoklamaBaslikEntity> AddYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            if (!IsYoklamaBaslik(yoklamaBaslikEntity).Success)
            {
                return IsYoklamaBaslik(yoklamaBaslikEntity);
            }
            var yoklamaBaslik = _yoklamaBaslikDal.Add(yoklamaBaslikEntity);
            return new ServiceResult<YoklamaBaslikEntity>(yoklamaBaslik, "Yoklama başlığı oluşturuldu", ServiceResultType.Created);
        }

        public ServiceResult<bool> DeleteYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            var yoklamaBaslik = _yoklamaBaslikDal.Delete(yoklamaBaslikEntity) > 0 ? true : false;
            return new ServiceResult<bool>(yoklamaBaslik, "Yoklama başlığı silindi", ServiceResultType.Success);
        }

        public ServiceResult<List<YoklamaBaslikModel>> GetYoklamaBaslikListesi()
        {
            var yoklamaBaslik = _yoklamaBaslikDal.GetList(y => y.Durum == DurumEnum.Aktif)
                .OrderBy(y => y.Baslik)
                .Select(y => new YoklamaBaslikModel
                {
                    Id = y.Id,
                    Baslik = y.Baslik.ToTitleCase()
                }).ToList();

            if (!yoklamaBaslik.Any())
            {
                return new ServiceResult<List<YoklamaBaslikModel>>(yoklamaBaslik, "Yoklama başlığı bulunamadı", ServiceResultType.NotFound);
            }
            return new ServiceResult<List<YoklamaBaslikModel>>(yoklamaBaslik, "Yoklama başlıkları listelendi", ServiceResultType.Success);
        }

        public ServiceResult<YoklamaBaslikEntity> UpdateYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            if (!IsYoklamaBaslik(yoklamaBaslikEntity).Success)
            {
                return IsYoklamaBaslik(yoklamaBaslikEntity);
            }
            yoklamaBaslikEntity.SonGuncelleyenId = CurrentUser.Id;
            yoklamaBaslikEntity.SonGuncellemeTarihi = DateTime.Now;
            var yoklamaBaslik = _yoklamaBaslikDal.Update(yoklamaBaslikEntity);
            return new ServiceResult<YoklamaBaslikEntity>(yoklamaBaslik, "Yoklama başlığı güncellendi", ServiceResultType.Success);

        }

        public ServiceResult<YoklamaBaslikEntity> GetYoklamaBaslik(byte yoklamaBaslikId)
        {
            var yoklamaBaslik =_yoklamaBaslikDal.Get(y => y.Id == yoklamaBaslikId);
            return new ServiceResult<YoklamaBaslikEntity>(yoklamaBaslik, "Yoklama başlığı getirildi", ServiceResultType.Success);
        }

        public ServiceResult<YoklamaBaslikEntity> IsYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            if (_yoklamaBaslikDal.Any(y => y.Id != yoklamaBaslikEntity.Id && y.Baslik == yoklamaBaslikEntity.Baslik))
            {
                return new ServiceResult<YoklamaBaslikEntity>(null, "Yoklama başlığı daha önceden kayıtlıdır", ServiceResultType.BadRequest);
            }
            return new ServiceResult<YoklamaBaslikEntity>(yoklamaBaslikEntity);
        }
    }
}
