using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
			return View();
		}

        // POST: /Hesaplar/Home/Login
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(KullaniciModel kullanici)
		{
			ModelState.Remove(nameof(kullanici.SifreOnay));
			if (ModelState.IsValid)
			{
				var mevcutKullanici = await _kullaniciService.GetItemAsync(kullanici.KullaniciAdi, kullanici.Sifre);
				if (mevcutKullanici is not null)
				{
					List<Claim> claims = new List<Claim>()
					{
						new Claim(ClaimTypes.Name, mevcutKullanici.KullaniciAdi),
						new Claim(ClaimTypes.Role, mevcutKullanici.RolOutput.Adi),
						new Claim(ClaimTypes.PrimarySid, mevcutKullanici.Id.ToString())
					};
					var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
					var principal = new ClaimsPrincipal(identity);
					await HttpContext.SignInAsync(principal);
					return RedirectToAction("Index", "Home", new { area = "" });
				}
				ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış!");
			}
			return View();
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

        // GET: /Hesaplar/Home/Logout
        public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index", "Home", new { area = "" });
		}

        // GET: /Hesaplar/Home/AccessDenied
		public IActionResult AccessDenied()
		{
			return View("_Error", "Bu işlem için yetkiniz bulunmamaktadır!");
		}
    }
}
