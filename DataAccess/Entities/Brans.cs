#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Brans : Record
    {
        [Required]
        [StringLength(150)]
        public string Adi { get; set; }



        // 1. yöntem:
        //public ICollection<Doktor> Doktorlar { get; set; }

        // 2. yöntem:
        public List<Doktor> Doktorlar { get; set; } // 1 to many has a relationship
    }
}
