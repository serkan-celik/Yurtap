using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Entity.Enums;
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

        public YoklamaBaslikEntity AddYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            IsYoklamaBaslik(yoklamaBaslikEntity);
            return _yoklamaBaslikDal.Add(yoklamaBaslikEntity);
        }

        public bool DeleteYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            return _yoklamaBaslikDal.Delete(yoklamaBaslikEntity) > 0 ? true : false;
        }

        public List<YoklamaBaslikModel> GetYoklamaBaslikListesi()
        {
            return _yoklamaBaslikDal.GetList(y => y.Durum == DurumEnum.Aktif)
                .OrderBy(y => y.Baslik.ToLower(CultureInfo.CurrentCulture))
                .Select(y => new YoklamaBaslikModel
                {
                    Id = y.Id,
                    Baslik = y.Baslik
                }).ToList();
        }

        public YoklamaBaslikEntity UpdateYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            yoklamaBaslikEntity.SonGuncelleyenId = CurrentUser.Id;
            yoklamaBaslikEntity.SonGuncellemeTarihi = DateTime.Now;
            return _yoklamaBaslikDal.Update(yoklamaBaslikEntity);
        }

        public YoklamaBaslikEntity GetYoklamaBaslik(byte yoklamaBaslikId)
        {
            return _yoklamaBaslikDal.Get(y => y.Id == yoklamaBaslikId);
        }

        public bool IsYoklamaBaslik(YoklamaBaslikEntity yoklamaBaslikEntity)
        {
            if (_yoklamaBaslikDal.Any(y => y.Baslik == yoklamaBaslikEntity.Baslik))
            {
                throw new Exception("Yoklama başlığı daha önceden kayıtlıdır!");
            }
            return true;
        }
    }
}
