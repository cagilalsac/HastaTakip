#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class BranslarController : Controller
    {
        // TODO: Add service injections here
        private readonly IBransService _bransService;
        private readonly IDoktorService _doktorService;

        public BranslarController(IBransService bransService, IDoktorService doktorService)
        {
            _bransService = bransService;
            _doktorService = doktorService;
        }

        // GET: Branslar
        public IActionResult Index()
        {
            List<BransModel> bransList = _bransService.Query().ToList(); // TODO: Add get collection service logic here
            return View(bransList);
        }

        // GET: Branslar/Details/5
        public IActionResult Details(int id)
        {
            BransModel brans = _bransService.Query().SingleOrDefault(b => b.Id == id); // TODO: Add get item service logic here
            if (brans == null)
            {
                return NotFound();
            }
            return View(brans);
        }

        // GET: Branslar/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["Doktorlar"] = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View();
        }

        // POST: Branslar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BransModel brans)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _bransService.Add(brans);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["Doktorlar"] = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View(brans);
        }

        // GET: Branslar/Edit/5
        public IActionResult Edit(int id)
        {
            BransModel brans = _bransService.Query().SingleOrDefault(b => b.Id == id); // TODO: Add get item service logic here
            if (brans == null)
            {
                return NotFound();
            }
			// TODO: Add get related items service logic here to set ViewData if necessary
			ViewData["Doktorlar"] = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

			return View(brans);
        }

        // POST: Branslar/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BransModel brans)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                var result = _bransService.Update(brans);
                if (result.IsSuccessful)
                {
                    TempData["Mesaj"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["Doktorlar"] = new MultiSelectList(_doktorService.Query().ToList(), "Id", "AdiSoyadiOutput");

            return View(brans);
        }

        // GET: Branslar/Delete/5
        public IActionResult Delete(int id)
        {
            BransModel brans = _bransService.Query().SingleOrDefault(b => b.Id == id); // TODO: Add get item service logic here
            if (brans == null)
            {
                return NotFound();
            }
            return View(brans);
        }

        // POST: Branslar/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            TempData["Mesaj"] = _bransService.Delete(id).Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
