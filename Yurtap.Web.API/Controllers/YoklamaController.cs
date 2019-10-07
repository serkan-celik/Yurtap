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
            var yoklama = _yoklamaBll.AddYoklama(yoklamaModel);
            if (yoklama.Success)
            {
                return Ok(yoklama);
            }
            return BadRequest(yoklama);
        }

        [HttpGet("GetYoklamaListeleri")]
        public ActionResult GetYoklamaListesi(DateTime? tarih)
        {
            var yoklamaListesi = _yoklamaBll.GetYoklamaListeleri(tarih);
            if (yoklamaListesi.Success)
            {
                return Ok(yoklamaListesi);
            }
            return NotFound(yoklamaListesi);
        }

        [HttpGet("GetYoklamaDetayById")]
        public ActionResult GetYoklamaDetayById(int id)
        {
            var yoklama = _yoklamaBll.GetYoklamaDetayById(id);
            if (yoklama.Success)
            {
                return Ok(yoklama);
            }
            return NotFound(yoklama);
        }

        [HttpGet("GetYoklamaListesi")]
        public ActionResult GetYoklamaListesi()
        {
            var yoklamaListesi = _yoklamaBll.GetYoklamaListesi();
            if (yoklamaListesi.Success)
            {
                return Ok(yoklamaListesi);
            }
            return NotFound(yoklamaListesi);
        }

        [HttpPut("UpdateYoklama")]
        public ActionResult UpdateYoklama(YoklamaModel yoklamaModel)
        {
            var yoklama = _yoklamaBll.UpdateYoklama(yoklamaModel);
            if (yoklama.Success)
            {
                return Ok(yoklama);
            }
            return BadRequest(yoklama);
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