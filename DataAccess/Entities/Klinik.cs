#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Klinik : Record
    {
        // 1. yöntem:
        //public string Adi { get; set; } = null!;

        // 2. yöntem:
        [Required] // Attribute, Data Annotation
        [StringLength(200)]
        public string Adi { get; set; }



        public string Aciklamasi { get; set; }

        public List<Doktor> Doktorlar { get; set; } // 0 to many has a relationship
    }
}
