using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Diagnostics;
using System.Text;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ~/Home/Index
        // ~/Home
        // ~/
        public IActionResult Index()
        {
            return View();
        }

        // ~/Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // ~/Home/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        // IActionResult // base'in implemente ettiði interface
        // |
        // ActionResult // base
        // |
        // ViewResult (View()) - ContentResult (Content()) - EmptyResult - FileContentResult (File()) - HttpResults - JavaScriptResult (JavaScript()) - JsonResult (Json()) - RedirectResults // subs
        // ~/Home/About
        public ViewResult About()
        {
            //return new ViewResult();
            return View();
        }

        // ~/Home/Contact
        public ActionResult Contact()
        {
            return View("Iletisim");
        }

        // ~/Home/Tarih
        public ContentResult Tarih()
        {
            //return new ContentResult()
            //{
            //    Content = "Bugünün tarihi: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"),
            //    ContentType = "text/plain",
            //    StatusCode = 200 // OK: baþarýlý
            //};
            return Content("Bugünün tarihi: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), "text/plain", Encoding.UTF8);
        }

        // ~/Home/Baslik
        public IActionResult Baslik()
        {
            return Content("<h1 style=\"color:blue;\">Hasta Takip</h1>", "text/html");
        }

        // ~/Home/Microsoft
        public RedirectResult Microsoft()
        {
            //return new RedirectResult("https://microsoft.com");
            return Redirect("https://microsoft.com");
        }
    }
}
