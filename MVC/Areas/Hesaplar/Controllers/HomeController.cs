using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Hesaplar.Controllers
{
	[Area("Hesaplar")]
	public class HomeController : Controller
	{
		private readonly IKullaniciService _kullaniciService;

        public HomeController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        // GET: /Hesaplar/Home/Login
        public IActionResult Login()
		{
			return Content("Login");
			//return View();
		}

        // GET: /Hesaplar/Home/Register
        public IActionResult Register()
		{
			return View();
		}

		// POST: /Hesaplar/Home/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(KullaniciModel kullanici)
		{
			if (ModelState.IsValid)
			{
				// 1. yöntem:
				//Task<Result> task = _kullaniciService.AddAsync(kullanici);
				//Result result = task.Result;
				// 2. yöntem:
				Result result = await _kullaniciService.AddAsync(kullanici);

				if (result.IsSuccessful)
				{
					return RedirectToAction(nameof(Login));
				}
				ModelState.AddModelError("", result.Message);
			}
			return View();
		}
    }
}
