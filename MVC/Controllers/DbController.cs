using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class DbController : Controller
    {
        public IActionResult Seed()
        {
            return View();
        }
    }
}
