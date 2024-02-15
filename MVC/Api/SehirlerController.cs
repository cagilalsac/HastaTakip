#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace MVC.Api
{
	// 1. yöntem:
	//[Route("api/Sehirler")]
	// 2. yöntem:
	[Route("api/[controller]")]
    [ApiController]
    public class SehirlerController : ControllerBase
    {
        // TODO: Add service injections here
        private readonly SehirServiceBase _sehirService;

        public SehirlerController(SehirServiceBase sehirService)
        {
            _sehirService = sehirService;
        }

        // GET: api/Sehirler
        [HttpGet]
        public IActionResult Get()
        {
            List<SehirModel> sehirList = _sehirService.GetList(); // TODO: Add get collection service logic here
            return Ok(sehirList); // 200 HTTP Status Code
        }

        // GET: api/Sehirler/5
        [HttpGet("{id}")] // route'u route değeri (id) üzerinden değiştirdik, bu action'ı ancak "api/Sehirler/5" şeklinde çağırabiliriz
		public IActionResult Get(int id)
        {
            SehirModel sehir = _sehirService.GetItem(id); // TODO: Add get item service logic here
			if (sehir == null)
			{
				// 1. yöntem:
                //return BadRequest(); // 400 HTTP Status Code
                // 2. yöntem:
				return NotFound(); // 404 HTTP Status Code
			}
			return Ok(sehir);
        }

        // GET: api/Sehirler/GetSehirler/5
        // 1. yöntem:
        //[HttpGet("GetSehirler/{ulkeId}")]
        // 2. yöntem:
        [HttpGet("{action}/{ulkeId}")] // route'u route değeri (ulkeId) üzerinden değiştirdik,
                                       // bu action'ı ancak "api/Sehirler/GetSehirler/5" şeklinde çağırabiliriz
		public IActionResult GetSehirler(int ulkeId)
        {
            List<SehirModel> sehirler = _sehirService.GetList(ulkeId);

            // 1. yöntem:
            //if (sehirler.Any())
            //  return Ok(sehirler); // 200 HTTP Status Code
            //return NotFound(); // 404 HTTP Status Code
            // 2. yöntem:
            return Ok(sehirler);
		}



        // Api controller yerine MVC controller oluşturmuş olsaydık:
        //public IActionResult GetSehirler(int ulkeId) // action
        //{
            //List<SehirModel> sehirler = _sehirService.GetList(ulkeId);
            //return Json(sehirler);
        //}
    }
}
