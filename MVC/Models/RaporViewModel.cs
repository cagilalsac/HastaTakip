#nullable disable

using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class RaporViewModel
    {
        public List<RaporModel> Rapor { get; set; }

        public RaporFiltreModel Filtre { get; set; }

        public SelectList Klinikler { get; set; }
    }
}
