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
        private readonly IYoklamaService _yoklamaService;
        private readonly IKisiBll _kisiBll;
        public YoklamaController(IYoklamaService yoklamaService, IKisiBll kisiBll)
        {
            _yoklamaService = yoklamaService;
            _kisiBll = kisiBll;
        }

        [HttpPost("AddYoklama")]
        public async Task<ActionResult> AddYoklama([FromBody]YoklamaModel yoklamaModel)
        {
            var yoklama = await _yoklamaService.AddYoklama(yoklamaModel);
            if (yoklama.Success)
            {
                return Created("",yoklama);
            }
            return BadRequest(yoklama);
        }

        [HttpGet("GetYoklamaListeleri")]
        public ActionResult GetYoklamaListesi(DateTime? tarih)
        {
            var yoklamaListesi = _yoklamaService.GetYoklamaListeleri(tarih);
            if (yoklamaListesi.Success)
            {
                return Ok(yoklamaListesi);
            }
            return NotFound(yoklamaListesi);
        }

        [HttpGet("GetYoklamaDetayById")]
        public ActionResult GetYoklamaDetayById(int id)
        {
            var yoklama = _yoklamaService.GetYoklamaDetayById(id);
            if (yoklama.Success)
            {
                return Ok(yoklama);
            }
            return NotFound(yoklama);
        }

        [HttpGet("GetYoklamaListesi")]
        public ActionResult GetYoklamaListesi()
        {
            var yoklamaListesi = _yoklamaService.GetYoklamaListesi();
            if (yoklamaListesi.Success)
            {
                return Ok(yoklamaListesi);
            }
            return NotFound(yoklamaListesi);
        }

        [HttpPut("UpdateYoklama")]
        public ActionResult UpdateYoklama(YoklamaModel yoklamaModel)
        {
            var yoklama = _yoklamaService.UpdateYoklama(yoklamaModel);
            if (yoklama.Success)
            {
                return Ok(yoklama);
            }
            return BadRequest(yoklama);
        }

        [HttpPost("ExportToExcelVakitlikYoklamaRaporu")]
        public IActionResult ExportToExcelVakitlikYoklamaRaporu(YoklamaModel yoklama)
        {
            var result = _yoklamaService.ExportToExcelVakitlikYoklamaRaporu(yoklama);
            if (result.Success)
            {
                return File(result.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            return NotFound(result);     
        }

        [HttpGet("ExportToExcelAylikYoklamaKatilimDurumuRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId,string yoklamaBaslik)
        {
            var result = _yoklamaService.ExportToExcelAylikYoklamaKatilimDurumuRaporu(tarih, yoklamaBaslikId, yoklamaBaslik);
            if (result.Success)
            {
                return File(result.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            return NotFound(result);
        }

        [HttpGet("ExportToExcelAylikYoklamaKatilimYuzdesiRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih)
        {
            var result = _yoklamaService.ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(tarih);
            if (result.Success)
            {
                return File(result.Result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            }
            return NotFound(result);
        }
    }
}