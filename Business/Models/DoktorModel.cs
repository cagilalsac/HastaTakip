#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DoktorModel : Record
    {
        #region Entity Özellikleri
        [Required]
        [StringLength(50)]
        public string Adi { get; set; }

        [Required]
        [StringLength(50)]
        public string Soyadi { get; set; }

        public bool UzmanMi { get; set; }

        public int? KlinikId { get; set; } 

        public int BransId { get; set; }
        #endregion

        #region Ekstra Özellikler
        public string AdiSoyadiOutput { get; set; }
        #endregion
    }
}
