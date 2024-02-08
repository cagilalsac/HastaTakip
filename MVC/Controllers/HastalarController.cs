#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    [Authorize(Roles = "admin")]
	public class HastalarController : Controller
    {
        // TODO: Add service injections here
        private readonly IHastaService _hastaService;
        private readonly IDoktorService _doktorService;

        public HastalarController(IHastaService hastaService, IDoktorService doktorService)
        {
            _hastaService = hastaService;
            _doktorService = doktorService;
        }

        // GET: Hastalar
        public IActionResult Index()
        {
            List<HastaModel> hastaList = _hastaService.Query().ToList(); // TODO: Add get collection service logic here
            return View(hastaList);
        }

        // GET: Hastalar/Details/5
        public IActionResult Details(int id)
        {
            HastaModel hasta = _hastaService.Query().SingleOrDefault(h => h.Id == id); // TODO: Add get item service logic here
            if (hasta == null)
            {
                //return NotFound();
                return View("_Error", "Hasta bulunamadı!");
            }
            return View(hasta);
        }

        // GET: Hastalar/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary

            //ViewBag.Doktorlar = new SelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput"); // drop down list: tek bir seçim
            ViewBag.Doktorlar = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput"); // list box: birden çok seçim
            
            return View();
        }

        // POST: Hastalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HastaModel hasta)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _hastaService.Add(hasta);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }

            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewBag.Doktorlar = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View(hasta);
        }

        // GET: Hastalar/Edit/5
        public IActionResult Edit(int id)
        {
            HastaModel hasta = _hastaService.Query().SingleOrDefault(h => h.Id == id); // TODO: Add get item service logic here
            if (hasta == null)
            {
                //return NotFound();
                return View("_Error", "Hasta bulunamadı!");
            }

            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["Doktorlar"] = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View(hasta);
        }

        // POST: Hastalar/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HastaModel hasta)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                var result = _hastaService.Update(hasta);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }

            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewBag.Doktorlar = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View(hasta);
        }

        // GET: Hastalar/Delete/5
        public IActionResult Delete(int id)
        {
            TempData["Mesaj"] = _hastaService.Delete(id).Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
