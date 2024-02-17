using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace MVC.Areas.Raporlar.Controllers
{
    [Area("Raporlar")]
	[Authorize(Roles = "admin")]
	public class HomeController : Controller
	{
		private readonly IRaporService _raporService;

		public HomeController(IRaporService raporService)
		{
			_raporService = raporService;
		}

		public IActionResult Index()
		{
			var rapor = _raporService.Query(true).ToList();
			return View(rapor);
		}

		public async Task ExportToExcel() // NuGet Kütüphaneleri: EPPlus veya SpreadSheetLight
		{
            var rapor = _raporService.Query(true).ToList();
			if (rapor.Any())
			{
				ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
				ExcelPackage excelPackage = new ExcelPackage();
				ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sayfa 1");
				excelWorksheet.Cells["A1"].LoadFromCollection(rapor, true);
				excelWorksheet.DeleteColumn(8, 4);
				excelWorksheet.Cells["A:AZ"].AutoFitColumns();
				byte[] data = excelPackage.GetAsByteArray();
                HttpContext.Response.Headers.Clear(); // HttpContext.Response yerine Response da kullanılabilir
                HttpContext.Response.Clear();
                HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Response.Headers.Add("content-length", data.Length.ToString());
                HttpContext.Response.Headers.Add("content-disposition", $"attachment; filename=\"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}_Rapor.xlsx\"");
                await HttpContext.Response.Body.WriteAsync(data, 0, data.Length);
            }
        }
	}
}
