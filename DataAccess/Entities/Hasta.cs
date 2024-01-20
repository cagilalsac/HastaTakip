#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Hasta : Record
    {
        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [Required]
        [StringLength(50)]
        public string Soyadi { get; set; }

        [StringLength(11)]
        public string KimlikNo { get; set; }

        public DateTime DogumTarihi { get; set; }

        public Cinsiyetler Cinsiyeti { get; set; }

        public decimal? Boyu { get; set; }

        public double? Kilosu { get; set; }

        public List<DoktorHasta> DoktorHastalar { get; set; } // many to many has a relationship
    }
}
