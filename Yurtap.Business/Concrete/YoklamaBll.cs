using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yurtap.Business.Abstract;
using Yurtap.Core.Enums;
using Yurtap.Core.Utilities.ExtensionMethods;
using Yurtap.DataAccess.Abstract;
using Yurtap.Entity;
using Yurtap.Entity.Models;
using Yurtap.Model.ReportModels.YoklamaModels;

namespace Yurtap.Business.Concrete
{
    public class YoklamaBll : BaseManager, IYoklamaBll
    {
        private readonly IYoklamaDal _yoklamaDal;
        private readonly IKisiBll _kisiBll;
        public YoklamaBll(IYoklamaDal yoklamaDal, IKisiBll kisiBll)
        {
            _yoklamaDal = yoklamaDal;
            _kisiBll = kisiBll;
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

        public YoklamaModel GetYoklamaDetayById(int id)
        {
            return _yoklamaDal.GetYoklamaDetayById(id);
        }


        public List<YoklamaModel> GetYoklamaListeleri(DateTime? tarih)
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

        public byte[] ExportToExcelVakitlikYoklamaRaporu(YoklamaModel yoklama)
        {

            var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeadrs = new string[]
       {
                "S.N",
                "ADI SOYADI",
                "DURUMU",
                "S.N",
                "ADI SOYADI",
                "DURUMU"
       };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                //Yeni bir excel dökümanı ve yoklama başlığı adında sekme oluşturuluyor
                var worksheet = package.Workbook.Worksheets.Add(yoklama.Baslik.ToUpper());

                //Yoklama başlıklarının tümü ortalanıyor
                //using (var cells = worksheet.Cells[4, 1, 4, 6]) //(1,1) (1,5)
                //{
                //    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                //}

                //Dökümanın yazıcı ayarları
                //worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                worksheet.PrinterSettings.HorizontalCentered = true;
                worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.BottomMargin = (decimal).5 / 2.54M;

                //Döküman yapılandırması
                worksheet.Cells["A1:F1"].Merge = true;
                worksheet.Cells["A2:F2"].Merge = true;
                worksheet.Cells["A3:B3"].Merge = true;
                worksheet.Cells["E3:F3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value = yoklama.Baslik.ToUpper() + " ÇİZELGESİ";
                worksheet.Cells["A1:A2"].Style.Font.Size = 16;
                worksheet.Cells["A3:B3"].Value = "Yoklama Görevlisi: " + kisi?.Ad + " " + kisi?.Soyad;
                worksheet.Cells["E3:F3"].Value = "Yoklama Tarihi: " + yoklama.Tarih.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cells["A1:F4"].Style.Font.Bold = true;
                worksheet.Cells["A5:B54"].Style.Font.Bold = true;
                worksheet.Cells["D5:E54"].Style.Font.Bold = true;

                //Döküman verileri hizalanıyor
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["A3:B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["E3:F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells["B5:B50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                worksheet.Cells["E5:E50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

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

                //Yoklama başlıkları ekleniyor
                for (var i = 0; i < comlumHeadrs.Count(); i++)
                {
                    worksheet.Cells[4, i + 1].Value = comlumHeadrs[i];
                }

                //Dökümana veriler ekleniyor.
                var j = 5;
                var k = 5;
                int rowCount = 1;
                int newRow = 100 - yoklama.YoklamaListesi.Count;
                for (int i = 0; i < newRow; i++)
                {
                    yoklama.YoklamaListesi.Add(new YoklamaListeModel());
                }

                foreach (var yoklamaItem in yoklama.YoklamaListesi)
                {
                    if (rowCount > 50) //50. kayıttan sonra veriler sayfanın sağ bölmesine yerleştirir.
                    {
                        worksheet.Cells["D" + k].Value = j - 4;
                        worksheet.Cells["E" + k].Value = string.Join(' ', yoklamaItem.Ad, yoklamaItem.Soyad).Insert(0, " ");
                        worksheet.Cells["F" + k].Value = yoklamaItem.Durum;
                        k++;
                        j++;
                        continue;
                    }

                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = string.Join(' ', yoklamaItem.Ad, yoklamaItem.Soyad).Insert(0, " ");
                    worksheet.Cells["B4"].AutoFitColumns(31);
                    worksheet.Cells["E4:E50"].AutoFitColumns(31);
                    worksheet.Cells["C" + j].Value = yoklamaItem.Durum;
                    //S.N-2 Kolon
                    worksheet.Cells["D4:D" + j].AutoFitColumns(3.5, 3.5);
                    j++;
                    rowCount++;
                }
                //S.N-1 Kolonu
                worksheet.Cells["A4:A50"].AutoFitColumns(3.5, 3.5);
                result = package.GetAsByteArray();
                return result;
            }
        }

        public byte[] ExportToExcelAylikYoklamaKatilimDurumuRaporu(DateTime tarih, byte yoklamaBaslikId, string yoklamaBaslik)
        {
            //var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeadrs = new string[]
       {
                "S.N",
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
                var worksheet = package.Workbook.Worksheets.Add("AYLIK YOKLAMALAR");

                //Yoklama başlıklarının tümü ortalanıyor
                using (var cells = worksheet.Cells[4, 1, 4, 33]) //(1,1) (1,5)
                {
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                //Dökümanın yazıcı ayarları
                //worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.HorizontalCentered = true;
                worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M;
                worksheet.PrinterSettings.BottomMargin = (decimal).5 / 2.54M;

                //Döküman yapılandırması
                worksheet.Cells["A1:AG1"].Merge = true;
                worksheet.Cells["A2:AG2"].Merge = true;
                worksheet.Cells["A3:AG3"].Merge = true;
                //worksheet.Cells["A4:F3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value = string.Join(' ', Enum.Parse<MonthsEnum>(tarih.Month.ToString()).GetDisplayName().ToUpper(), tarih.Year.ToString(), yoklamaBaslik.ToUpper(), "ÇİZELGESİ");
                //worksheet.Cells.Style.Font.Name = "Calibri";
                worksheet.Cells["A1:AG2"].Style.Font.Size = 16;
                worksheet.Cells.Style.Font.Bold = true;
                worksheet.Cells["A1:AG2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

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
                worksheet.Cells["A1:AG3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:AG3"].Style.Border.Left.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:AG3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:AG3"].Style.Border.Top.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:AG3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;



                //Dökümana veriler ekleniyor.
                var j = 5;
                var k = 5;
                int rowCount = 1;
                var yoklamaListesi = _yoklamaDal.GetYoklamaKatilimDurumuAylikRaporListesi(tarih, yoklamaBaslikId);


                foreach (var yoklama in yoklamaListesi)
                {

                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = yoklama.AdSoyad.Insert(0, " ");
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
                worksheet.Cells["A4:A100"].AutoFitColumns(3.5, 3.5);
                worksheet.Cells["B4:B100"].AutoFitColumns();
                worksheet.Cells["A5:A50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:AG100"].AutoFitColumns(3.5, 3.5);
                worksheet.Cells["C4:AG100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["C4:C50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["D5:D100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells["F4:F50"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //worksheet.Cells.AutoFitColumns();

                result = package.GetAsByteArray();
                return result;
            }
        }

        public byte[] ExportToExcelAylikYoklamaKatilimYuzdesiRaporu(DateTime tarih)
        {
            //var kisi = _kisiBll.GetKisi(yoklama.EkleyenId);
            var comlumHeaders = new List<string>
       {
                "S.N",
                "ADI SOYADI"
       };

            byte[] result;

            using (var package = new ExcelPackage())
            {
                //Yeni bir excel dökümanı ve yoklama başlığı adında sekme oluşturuluyor
                var worksheet = package.Workbook.Worksheets.Add("AYLIK YOKLAMALARA KATILIM ORANLARI");

                //Yoklama başlıklarının tümü ortalanıyor
                using (var cells = worksheet.Cells[4, 1, 4, 33]) //(1,1) (1,5)
                {
                    cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                //Dökümanın yazıcı ayarları
                //worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                worksheet.PrinterSettings.Orientation = eOrientation.Portrait;
                worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M;
                //worksheet.PrinterSettings.RightMargin = 1M;
                //worksheet.PrinterSettings.LeftMargin = 1M;
                worksheet.PrinterSettings.HorizontalCentered = true;
                worksheet.PrinterSettings.BottomMargin = (decimal).5 / 2.54M;

                //Döküman yapılandırması
                worksheet.Cells["A1:H1"].Merge = true;
                worksheet.Cells["A2:H2"].Merge = true;
                worksheet.Cells["A3:H3"].Merge = true;

                worksheet.Cells["A1"].Value = "MERKEZ ÖĞRENCİ YURDU";
                worksheet.Cells["A2"].Value = string.Join(' ', Enum.Parse<MonthsEnum>(tarih.Month.ToString()).GetDisplayName().ToUpper(), tarih.Year.ToString(), "YOKLAMALARA KATILIM ORANLARI ÇİZELGESİ");
                worksheet.Cells["A1:A2"].Style.Font.Size = 16;
                //worksheet.Cells["A3:B3"].Value = "Personel: " + kisi?.Ad + " " + kisi?.Soyad;
                worksheet.Cells["A3:B3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                //worksheet.Cells["E3:F3"].Value = "Tarih: " + yoklama.Tarih.ToString("dd.MM.yyyy HH:mm");
                worksheet.Cells["E3:F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                worksheet.Cells["A1:H4"].Style.Font.Bold = true;
                worksheet.Cells["A5:B100"].Style.Font.Bold = true;
                worksheet.Cells["A1:A2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                //Tüm döküman hücrelerine kenarlık ekleniyor.
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                //Döküman başlıklarının kenarlıkları kaldırılıyor.
                worksheet.Cells["A1:H3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:H3"].Style.Border.Left.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:H3"].Style.Border.Right.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:H3"].Style.Border.Top.Style = ExcelBorderStyle.None;
                worksheet.Cells["A1:H3"].Style.Border.Bottom.Style = ExcelBorderStyle.None;

                //Dökümana veriler ekleniyor.
                var j = 5;
                var k = 5;
                int column = 3;


                var yoklamaListesi = _yoklamaDal.GetYoklamaKatilimYuzdesiAylikRaporListesi(tarih);
                foreach (var item in yoklamaListesi[0].YoklamaIstatistikleri)
                {
                    comlumHeaders.Add(item.YoklamaBaslik.Substring(0, item.YoklamaBaslik.IndexOf(' ')).ToUpper());
                }

                //Yoklama başlıkları ekleniyor
                comlumHeaders.Add("GENEL");
                for (var i = 0; i < comlumHeaders.Count(); i++)
                {
                    worksheet.Cells[4, i + 1].Value = comlumHeaders[i];
                }

                foreach (var yoklama in yoklamaListesi)
                {
                    worksheet.Cells["A" + j].Value = j - 4;
                    worksheet.Cells["B" + j].Value = yoklama.AdSoyad.Insert(0, " ");
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
                worksheet.Cells["C4:H100"].AutoFitColumns();//AutoFitColumns(3.5, 3.5);
                worksheet.Cells["B4:B100"].AutoFitColumns();
                worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B5:B100"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                //worksheet.Cells.AutoFitColumns();

                result = package.GetAsByteArray();
                return result;
            }
        }
    }
}

