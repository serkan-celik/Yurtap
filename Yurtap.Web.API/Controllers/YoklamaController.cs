using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Yurtap.Business.Abstract;
using Yurtap.Core.Enums;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.Core.Web.Mvc;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Model.ReportModels.YoklamaModels;

namespace Yurtap.Web.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class YoklamaController : BaseControllor
    {
        private readonly IYoklamaBll _yoklamaBll;
        private readonly IKisiBll _kisiBll;
        public YoklamaController(IYoklamaBll yoklamaBll, IKisiBll kisiBll)
        {
            _yoklamaBll = yoklamaBll;
            _kisiBll = kisiBll;
        }

        [HttpPost("AddYoklama")]
        public ActionResult AddYoklama([FromBody]YoklamaModel yoklamaModel)
        {
            try
            {
                _yoklamaBll.AddYoklama(yoklamaModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaModel);
        }

        [HttpGet("GetYoklamaListeleri")]
        public ActionResult GetYoklamaListesi(DateTime? tarih)
        {
            List<YoklamaModel> yoklamaListesi=null;
            try
            {
                yoklamaListesi = _yoklamaBll.GetYoklamaListeleri(tarih);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaListesi);
        }

        [HttpGet("GetYoklamaDetayById")]
        public ActionResult GetYoklamaDetayById(int id)
        {
            YoklamaModel yoklama = null;
            try
            {
                yoklama = _yoklamaBll.GetYoklamaDetayById(id);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok(yoklama);
        }

        [HttpGet("GetYoklamaListesi")]
        public ActionResult GetYoklamaListesi()
        {
            List<YoklamaListeModel> yoklamaListesi;
            try
            {
                yoklamaListesi = _yoklamaBll.GetYoklamaListesi();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaListesi);
        }

        [HttpPut("UpdateYoklama")]
        public ActionResult UpdateYoklama(YoklamaModel yoklamaModel)
        {
            YoklamaEntity yoklamaEntity = null;
            try
            {
                yoklamaEntity = _yoklamaBll.UpdateYoklama(yoklamaModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaEntity);
        }

        [HttpPost("ExportToExcelVakitlikYoklamaRaporu")]
        public IActionResult ExportToExcelVakitlikYoklamaRaporu(YoklamaModel yoklama)
        {
            var result = _yoklamaBll.ExportToExcelVakitlikYoklamaRaporu(yoklama);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("ExportToExcelAylikYoklamaKatilimDurumuRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId,string yoklamaBaslik)
        {
            var result = _yoklamaBll.ExportToExcelAylikYoklamaKatilimDurumuRaporu(tarih, yoklamaBaslikId, yoklamaBaslik);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("ExportToExcelAylikYoklamaKatilimYuzdesiRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih)
        {
            var result = _yoklamaBll.ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(tarih);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}