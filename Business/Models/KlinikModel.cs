#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KlinikModel : Record
    {
        [Required]
        [StringLength(200)]
        public string Adi { get; set; }

        public string Aciklamasi { get; set; }
    }
}
