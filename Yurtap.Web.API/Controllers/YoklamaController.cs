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

        [HttpGet("GetYoklamaKatilimDurumuAylikRaporListesi")]
        public ActionResult GetYoklamaKatilimDurumuAylikRaporListesi(DateTime tarih, byte yoklamaBaslikId)
        {
            List<YoklamaAylikKatilimModel> yoklamaListesi = null;
            try
            {
                yoklamaListesi = _yoklamaBll.GetYoklamaKatilimDurumuAylikRaporListesi(tarih, yoklamaBaslikId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaListesi);
        }

        [HttpGet("GetYoklamaYuzdelikKatilimAylikRaporListesi")]
        public ActionResult GetYoklamaYuzdelikKatilimAylikRaporListesi(DateTime tarih)
        {
            List<YoklamaAylikYuzdelikKatilimModel> yoklamaListesi = null;
            try
            {
                yoklamaListesi = _yoklamaBll.GetYoklamaYuzdelikKatilimAylikRaporListesi(tarih);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(yoklamaListesi);
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

        [HttpGet("ExportToExcelAylikYoklamaKatilimDurumuRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId)
        {

            //var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeadrs = new string[]
       {
                "NO",
                "ADI SOYADI",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "10",
                "11",
                "12",
                "13",
                "14",
                "15",
                "16",
                "17",
                "18",
                "19",
                "20",
                "21",
                "22",
                "23",
                "24",
                "25",
                "26",
                "27",
                "28",
                "29",
                "30",
                "31"
       };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                //Yeni bir excel dökümanı ve yoklama başlığı adında sekme oluşturuluyor
                var worksheet = package.Workbook.Worksheets.Add("Aylık Katılım Raporu");

                //Yoklama başlıklarının tümü ortalanıyor
                using (var cells = worksheet.Cells[4, 1, 4, 33]) //(1,1) (1,5)
                {
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                //Dökümanın yazıcı ayarları
                //worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4Plus;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

                //Döküman yapılandırması
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A3:B3"].Merge = true;
                worksheet.Cells["E3:F3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value =   "AYYLIK ÇİZELGESİ";
                worksheet.Cells["A1:A2"].Style.Font.Size = 16;
                //worksheet.Cells["A3:B3"].Value = "Personel: " + kisi?.Ad + " " + kisi?.Soyad;
                worksheet.Cells["A3:B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                //worksheet.Cells["E3:F3"].Value = "Tarih: " + yoklama.Tarih.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cells["E3:F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells.Style.Font.Bold = true;
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
               var yoklamaListesi = _yoklamaBll.GetYoklamaKatilimDurumuAylikRaporListesi(tarih, yoklamaBaslikId);


                foreach (var yoklama in yoklamaListesi)
                {
                  
                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = yoklama.AdSoyad;
                    worksheet.Cells["C" + j].Value = yoklama.Gun1;
                    worksheet.Cells["D" + j].Value = yoklama.Gun2;
                    worksheet.Cells["E" + j].Value = yoklama.Gun3;
                    worksheet.Cells["F" + j].Value = yoklama.Gun4;
                    worksheet.Cells["G" + j].Value = yoklama.Gun5;
                    worksheet.Cells["H" + j].Value = yoklama.Gun6;
                    worksheet.Cells["I" + j].Value = yoklama.Gun7;
                    worksheet.Cells["J" + j].Value = yoklama.Gun8;
                    worksheet.Cells["K" + j].Value = yoklama.Gun9;
                    worksheet.Cells["L" + j].Value = yoklama.Gun10;
                    worksheet.Cells["M" + j].Value = yoklama.Gun11;
                    worksheet.Cells["N" + j].Value = yoklama.Gun12;
                    worksheet.Cells["O" + j].Value = yoklama.Gun13;
                    worksheet.Cells["P" + j].Value = yoklama.Gun14;
                    worksheet.Cells["Q" + j].Value = yoklama.Gun15;
                    worksheet.Cells["R" + j].Value = yoklama.Gun16;
                    worksheet.Cells["S" + j].Value = yoklama.Gun17;
                    worksheet.Cells["T" + j].Value = yoklama.Gun18;
                    worksheet.Cells["U" + j].Value = yoklama.Gun19;
                    worksheet.Cells["V" + j].Value = yoklama.Gun20;
                    worksheet.Cells["W" + j].Value = yoklama.Gun21;
                    worksheet.Cells["X" + j].Value = yoklama.Gun22;
                    worksheet.Cells["Y" + j].Value = yoklama.Gun23;
                    worksheet.Cells["Z" + j].Value = yoklama.Gun24;
                    worksheet.Cells["AA" + j].Value = yoklama.Gun25;
                    worksheet.Cells["AB" + j].Value = yoklama.Gun26;
                    worksheet.Cells["AC" + j].Value = yoklama.Gun27;
                    worksheet.Cells["AD" + j].Value = yoklama.Gun28;
                    worksheet.Cells["AE" + j].Value = yoklama.Gun29;
                    worksheet.Cells["AF" + j].Value = yoklama.Gun30;
                    worksheet.Cells["AG" + j].Value = yoklama.Gun31;
                    j++;
                }

                //Döküman verilerinin tümü ortalalanıyor.
                worksheet.Cells["A5:A50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:AG100"].AutoFitColumns(3.5,3.5);
                worksheet.Cells["C4:AG100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:C50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D5:D100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B4:B100"].AutoFitColumns();
                worksheet.Cells["F4:F50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //worksheet.Cells.AutoFitColumns();

                result = package.GetAsByteArray();
            }


            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet("ExportToExcelAylikYoklamaKatilimYuzdesiRaporu")]
        public ActionResult ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih)
        {

            //var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeaders = new List<string>
       {
                "NO",
                "ADI SOYADI"
       };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                //Yeni bir excel dökümanı ve yoklama başlığı adında sekme oluşturuluyor
                var worksheet = package.Workbook.Worksheets.Add("Aylık Katılım Raporu");

                //Yoklama başlıklarının tümü ortalanıyor
                using (var cells = worksheet.Cells[4, 1, 4, 33]) //(1,1) (1,5)
                {
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                //Dökümanın yazıcı ayarları
                //worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4Plus;
                worksheet.PrinterSettings.Orientation = eOrientation.Portrait;

                //Döküman yapılandırması
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A3:B3"].Merge = true;
                worksheet.Cells["E3:F3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value = "AYYLIK ÇİZELGESİ";
                worksheet.Cells["A1:A2"].Style.Font.Size = 16;
                //worksheet.Cells["A3:B3"].Value = "Personel: " + kisi?.Ad + " " + kisi?.Soyad;
                worksheet.Cells["A3:B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                //worksheet.Cells["E3:F3"].Value = "Tarih: " + yoklama.Tarih.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cells["E3:F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells["A1:G4"].Style.Font.Bold = true;
                worksheet.Cells["A5:B100"].Style.Font.Bold = true;
                worksheet.Cells["A1:A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //Tüm döküman hücrelerine kenarlık ekleniyor.
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Döküman başlıklarının kenarlıkları kaldırılıyor.
                worksheet.Cells["A1:F3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Left.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Top.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:F3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;

                //Dökümana veriler ekleniyor.
                var j = 5;
                var k = 5;
                int column = 3;


                var yoklamaListesi = _yoklamaBll.GetYoklamaYuzdelikKatilimAylikRaporListesi(tarih);
                foreach (var item in yoklamaListesi[0].YoklamaIstatistikleri)
                {
                    comlumHeaders.Add(item.YoklamaBaslik.Substring(0, item.YoklamaBaslik.IndexOf(' ')).ToUpper());
                }

                //Yoklama başlıkları ekleniyor
                comlumHeaders.Add("Genel Katılım");
                for (var i = 0; i < comlumHeaders.Count(); i++)
                {
                    worksheet.Cells[4, i + 1].Value = comlumHeaders[i];
                }

                foreach (var yoklama in yoklamaListesi)
                {
                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = yoklama.AdSoyad;
                    foreach (var item in yoklama.YoklamaIstatistikleri)
                    {
                        if (column > comlumHeaders.Count())
                        {
                            break;
                        }
                        worksheet.Cells[j, column].Value = '%' + item.KatilimYuzdesi;
                        ++column;
                    }
                    worksheet.Cells[j, column].Value = '%' + yoklama.GenelKatilimYuzdesi;
                    column = 3;
                    ++j;

                }

                //Döküman verilerinin tümü ortalalanıyor.
                worksheet.Cells["A4:A100"].AutoFitColumns(3.5, 3.5);
                worksheet.Cells["A5:A50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:AG100"].AutoFitColumns();//AutoFitColumns(3.5, 3.5);
                worksheet.Cells["C4:AG100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:C50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D5:D100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B4:B100"].AutoFitColumns();
                worksheet.Cells["F4:F50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //worksheet.Cells.AutoFitColumns();

                result = package.GetAsByteArray();
            }


            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
    public class Yoklama
    {
        public DateTime Tarih { get; set; }
    }
}