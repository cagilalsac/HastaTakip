#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Doktor : Record
    {
        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [Required]
        [StringLength(50)]
        public string Soyadi { get; set; }

        public bool UzmanMi { get; set; }

        public int? KlinikId { get; set; } // veritabanı 0 to many ilişkisi

        public Klinik Klinik { get; set; } // 0 to many has a relationship

        public int BransId { get; set; } // veritabanı 1 to many ilişkisi

        public Brans Brans { get; set; } // 1 to many has a relationship

        public List<DoktorHasta> DoktorHastalar { get; set; } // many to many has a relationship
    }
}
