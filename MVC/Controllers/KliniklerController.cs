#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class KliniklerController : Controller
    {
        // TODO: Add service injections here
        private readonly IKlinikService _klinikService;

        public KliniklerController(IKlinikService klinikService)
        {
            _klinikService = klinikService;
        }

        // GET: Klinikler
        public IActionResult Index()
        {
            List<KlinikModel> klinikList = _klinikService.Query().ToList(); // TODO: Add get collection service logic here
            return View(klinikList);
        }

        // GET: Klinikler/GetJson
        public JsonResult GetListJson()
        {
            var klinikler = _klinikService.Query().ToList();
            return Json(klinikler);
        }

        // GET: Klinikler/Details/5
        public IActionResult Details(int id)
        {
            // koşula göre bulduğu ilk kaydı döner, eğer kaydı bulamazsa exception fırlatır
            //KlinikModel klinik = _klinikService.Query().First(k => k.Id == id);

            // koşula göre bulduğu ilk kaydı döner, eğer kaydı bulamazsa null döner
            //KlinikModel klinik = _klinikService.Query().FirstOrDefault(k => k.Id == id);

            // koşula göre bulduğu son kaydı döner, eğer kaydı bulamazsa exception fırlatır,
            // sorgularda genellikle kullanılmaz
            //KlinikModel klinik = _klinikService.Query().Last(k => k.Id == id);

            // koşula göre bulduğu son kaydı döner, eğer kaydı bulamazsa null döner,
            // sorgularda genellikle kullanılmaz
            //KlinikModel klinik = _klinikService.Query().LastOrDefault(k => k.Id == id);

            // koşula göre bulduğu tek kaydı döner, eğer kaydı bulamazsa exception fırlatır, birden çok kayıt bulursa exception fırlatır
            //KlinikModel klinik = _klinikService.Query().Single(k => k.Id == id);

            // önce kayıtlar where ile filtrelenir, dönen koleksiyon sonucu üzerinden tek bir kayıt dönülür
            //KlinikModel klinik = _klinikService.Query().Where(k => k.Id == id).SingleOrDefault();

            // 1. yöntem:
            // koşula göre bulduğu tek kaydı döner, eğer kaydı bulamazsa null döner, birden çok kayıt bulursa exception fırlatır
            //KlinikModel klinik = _klinikService.Query().SingleOrDefault(k => k.Id == id); // TODO: Add get item service logic here

            // 2. yöntem:
            KlinikModel klinik = _klinikService.GetItem(id);

            if (klinik == null)
            {
                return NotFound(); // 404 (Not found) HTTP Status Code
            }
            return View(klinik); // 200 (Ok) HTTP Status Code
        }

        public IActionResult GetItemJson(int id)
        {
            var klinik = _klinikService.Query().SingleOrDefault(k => k.Id == id);
            return Json(klinik);
        }

        // GET: Klinikler/Create
        //[HttpGet] // default: GET
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Klinikler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(KlinikModel klinik)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                return RedirectToAction(nameof(Index));
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(klinik);
        }

        // GET: Klinikler/Edit/5
        public IActionResult Edit(int id)
        {
            KlinikModel klinik = null; // TODO: Add get item service logic here
            if (klinik == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(klinik);
        }

        // POST: Klinikler/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(KlinikModel klinik)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                return RedirectToAction(nameof(Index));
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(klinik);
        }

        // GET: Klinikler/Delete/5
        public IActionResult Delete(int id)
        {
            KlinikModel klinik = null; // TODO: Add get item service logic here
            if (klinik == null)
            {
                return NotFound();
            }
            return View(klinik);
        }

        // POST: Klinikler/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
