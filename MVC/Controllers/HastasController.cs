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
    public class HastasController : Controller
    {
        private readonly Db _context;

        public HastasController(Db context)
        {
            _context = context;
        }

        // GET: Hastas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hastalar.ToListAsync());
        }

        
    }
}
