using Newtonsoft.Json;
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
    public class YoklamaBll : BaseManager, IYoklamaBll
    {
        private readonly IYoklamaDal _yoklamaDal;
        public YoklamaBll(IYoklamaDal yoklamaDal)
        {
            _yoklamaDal = yoklamaDal;
        }

        public YoklamaModel AddYoklama(YoklamaModel yoklamaModel)
        {
            IsYoklama(yoklamaModel);
            var yoklamaEntity = _yoklamaDal.Add(new YoklamaEntity()
            {
                YoklamaBaslikId = yoklamaModel.YoklamaBaslikId,
                Tarih = yoklamaModel.Tarih,
                Liste = JsonConvert.SerializeObject(yoklamaModel.YoklamaListesi)
            });
            yoklamaModel.Id = yoklamaEntity.Id;
            return yoklamaModel;
        }

        public void ExportToExcel(YoklamaModel yoklamaModel)
        {
            throw new NotImplementedException();
        }

        public YoklamaModel GetYoklamaDetayById(int id)
        {
            return _yoklamaDal.GetYoklamaDetayById(id);
        }

        public List<YoklamaModel> GetYoklamaListeleri(DateTime tarih)
        {
            return _yoklamaDal.GetYoklamaListeleriByTarih(tarih);
        }

        public List<YoklamaListeModel> GetYoklamaListesi()
        {
            return _yoklamaDal.GetYoklamaListesi();
        }

        public bool IsYoklama(YoklamaModel yoklamaModel)
        {
            bool ayniZamanMi = _yoklamaDal.Any(y => y.Tarih == yoklamaModel.Tarih);
            bool ayniYoklamaMi = _yoklamaDal.Any(y => y.Tarih == yoklamaModel.Tarih && (y.Liste == JsonConvert.SerializeObject(yoklamaModel.YoklamaListesi)));

            if ((yoklamaModel.Id == 0 && ayniZamanMi) || (yoklamaModel.Id > 0 && ayniYoklamaMi))
            {
                throw new Exception("Yoklama daha önceden kayıtlıdır!");
            }
            return true;
        }

        public YoklamaEntity UpdateYoklama(YoklamaModel yoklamaModel)
        {
            IsYoklama(yoklamaModel);
            return _yoklamaDal.Update(new YoklamaEntity()
            {
                Id = yoklamaModel.Id,
                YoklamaBaslikId = yoklamaModel.YoklamaBaslikId,
                Tarih = yoklamaModel.Tarih,
                Liste = JsonConvert.SerializeObject(yoklamaModel.YoklamaListesi),
                SonGuncelleyenId = CurrentUser.Id,
                SonGuncellemeTarihi = DateTime.Now
        });
        }
    }
}

