using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Utilities.Bases;
using System.Security.Claims;

namespace MVC.Controllers
{
	[Authorize(Roles = "kullanıcı")]
    public class FavoriDoktorlarController : Controller
    {
        private readonly IDoktorService _doktorService;
        private readonly FavoriDoktorlarSessionUtilBase _favoriDoktorlarSessionUtil;

		public FavoriDoktorlarController(IDoktorService doktorService, FavoriDoktorlarSessionUtilBase favoriDoktorlarSessionUtil)
        {
            _doktorService = doktorService;
            _favoriDoktorlarSessionUtil = favoriDoktorlarSessionUtil;
        }

        // GET: /FavoriDoktorlar/Index
        public IActionResult Index()
        {
            return View(_favoriDoktorlarSessionUtil.GetSession());
        }

        // GET: /FavoriDoktorlar/Add
        public IActionResult Add(int doktorId)
        {
            int hastaId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.PrimarySid).Value);
            DoktorModel doktor = _doktorService.Query().SingleOrDefault(d => d.Id == doktorId);
            if (doktor is null)
                return View("_Error", "Doktor bulunamadı.");
            List<FavoriDoktorModel> favoriDoktorlar = _favoriDoktorlarSessionUtil.GetSession();
            if (!favoriDoktorlar.Any(fd => fd.HastaId == hastaId && fd.DoktorId == doktorId))
            {
                FavoriDoktorModel favoriDoktor = new FavoriDoktorModel()
                {
                    DoktorAdiSoyadi = doktor.AdiSoyadiOutput,
                    DoktorId = doktor.Id,
                    HastaId = hastaId
                };
                favoriDoktorlar.Add(favoriDoktor);
                _favoriDoktorlarSessionUtil.SetSession(favoriDoktorlar);
                TempData["Mesaj"] = $"\"{doktor.AdiSoyadiOutput}\" favorilere eklendi.";
            }
            return RedirectToAction("Index", "Doktorlar");
        }
    }
}
