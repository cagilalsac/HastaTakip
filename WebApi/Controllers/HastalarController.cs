#nullable disable
using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

//Generated from Custom Template.
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HastalarController : ControllerBase
    {
        // TODO: Add service injections here
        private readonly IHastaService _hastaService;

        public HastalarController(IHastaService hastaService)
        {
            _hastaService = hastaService;
        }

        // GET: api/Hastalar
        [HttpGet]
        public IActionResult Get()
        {
            List<HastaModel> hastaList = _hastaService.Query().ToList(); // TODO: Add get collection service logic here
            return Ok(hastaList); // 200 HTTP Status Code
        }

        // GET: api/Hastalar/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            HastaModel hasta = _hastaService.Query().SingleOrDefault(q => q.Id == id); // TODO: Add get item service logic here
			if (hasta == null)
            {
                return NotFound(); // 404 HTTP Status Code
            }
			return Ok(hasta);
        }

		// POST: api/Hastalar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post(HastaModel hasta) // Create
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _hastaService.Add(hasta);
                if (result.IsSuccessful)
			        return CreatedAtAction("Get", new { id = hasta.Id }, hasta);
                ModelState.AddModelError(nameof(hasta), result.Message);
            }
            return BadRequest(ModelState); // 400 HTTP Status Code
        }

        // PUT: api/Hastalar
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put(HastaModel hasta) // Edit -> Update
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                var result = _hastaService.Update(hasta);
                if (result.IsSuccessful)
                {
                    //return NoContent(); // 204 HTTP Status Code
                    return Ok(hasta);
                }
                ModelState.AddModelError(nameof(hasta), result.Message);
            }
            return BadRequest(ModelState);
        }

        // DELETE: api/Hastalar/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // TODO: Add delete service logic here
            var result = _hastaService.Delete(id);
            if (result.IsSuccessful)
                return NoContent();
            return BadRequest(result.Message);
        }
	}
}
