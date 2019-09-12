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
using Yurtap.Core.Web.Mvc;
using Yurtap.Entity;
using Yurtap.Entity.Models;

namespace Yurtap.Web.API.Controllers
{
    [Authorize]
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
        public ActionResult GetYoklamaListesi(DateTime tarih)
        {
            List<YoklamaModel> yoklamaListesi=null;
            try
            {
                yoklamaListesi = _yoklamaBll.GetYoklamaListeleri(tarih);
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
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

        [HttpPost("ExportToExcelYoklama")]
        public IActionResult ExportToExcelYoklama(YoklamaModel yoklama)
        {

            var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeadrs = new string[]
       {
                "NO",
                "ADI SOYADI",
                "DURUMU",
                "NO",
                "ADI SOYADI",
                "DURUMU"
       };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                //Yeni bir excel dökümanı ve yoklama başlığı adında sekme oluşturuluyor
                var worksheet = package.Workbook.Worksheets.Add(yoklama.Baslik);

                //Yoklama başlıklarının tümü ortalanıyor
                using (var cells = worksheet.Cells[4, 1, 4, 6]) //(1,1) (1,5)
                {
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }
            
                //Dökümanın yazıcı ayarları
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;

                //Döküman yapılandırması
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A3:B3"].Merge = true;
                worksheet.Cells["E3:F3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value =  yoklama.Baslik.ToUpper() + " ÇİZELGESİ" ;
                worksheet.Cells["A1:A2"].Style.Font.Size = 16;
                worksheet.Cells["A3:B3"].Value = "Personel: " + kisi?.Ad + " " + kisi?.Soyad;
                worksheet.Cells["A3:B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["E3:F3"].Value = "Tarih: " + yoklama.Tarih.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cells["E3:F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells["A1:F4"].Style.Font.Bold = true;
                worksheet.Cells["A1:A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //Tüm döküman hücrelerine kenarlık ekleniyor.
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Yoklama başlıkları ekleniyor
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[4, i + 1].Value = comlumHeadrs[i];
                }

                //Döküman başlıklarının kenarlıkları kaldırılıyor.
                worksheet.Cells["A1:F3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Left.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Top.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;

                //Dökümana veriler ekleniyor.
                var j = 5;
                var k = 5;
                int rowCount = 1;
                foreach (var employee in yoklama.YoklamaListesi)
                {
                    if (rowCount > 50) //50. kayıttan sonra veriler sayfanın sağ bölmesine yerleştirir.
                    {
                        worksheet.Cells["D" + k].Value = j-4;
                        worksheet.Cells["E" + k].Value = employee.Ad + " " + employee.Soyad;
                        worksheet.Cells["F" + k].Value = employee.Durum;
                        k++;
                        j++;
                        continue;
                    }

                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = employee.Ad + " " + employee.Soyad;
                    worksheet.Cells["C" + j].Value = employee.Durum;
                    worksheet.Cells["E" + j].Value = "xxxxxxxxxxxxxxxxxxxxxx";
                    worksheet.Cells["E"+ j].Style.Font.Color.SetColor(Color.White);
                    j++;
                    rowCount++;
                }

                //Döküman verilerinin tümü ortalalanıyor.
                worksheet.Cells["A5:A50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B4:B50"].AutoFitColumns();
                worksheet.Cells["C4:C50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D5:D50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //worksheet.Cells["E5"].AutoFitColumns();
                worksheet.Cells["F4:F50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells.AutoFitColumns();

                result = package.GetAsByteArray();
            }

 
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}