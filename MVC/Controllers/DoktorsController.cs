using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace MVC.Controllers
{
    public class DoktorsController : Controller
    {
        private readonly Db _context;

        public DoktorsController(Db context)
        {
            _context = context;
        }

        // GET: Doktors
        public async Task<IActionResult> Index()
        {
            var db = _context.Doktorlar.Include(d => d.Brans).Include(d => d.Klinik);
            return View(await db.ToListAsync());
        }

        
    }
}
