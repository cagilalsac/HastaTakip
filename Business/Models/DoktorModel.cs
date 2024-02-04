#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class DoktorModel : Record
    {
        #region Entity Özellikleri
        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} en az {2} en çok {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} en az {2} en çok {1} karakter olmalıdır!")]
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }

        [DisplayName("Uzman")]
        public bool UzmanMi { get; set; }

        [DisplayName("Klinik")]
        public int? KlinikId { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [DisplayName("Branş")]
        public int? BransId { get; set; }
        #endregion

        #region Ekstra Özellikler
        [DisplayName("Adı Soyadı")]
        public string AdiSoyadiOutput { get; set; }

        [DisplayName("Durumu")]
        public string UzmanMiOutput { get; set; }

        [DisplayName("Klinik")]
        public string KlinikOutput { get; set; }

        [DisplayName("Branş")]
        public string BransOutput { get; set; }

        [DisplayName("Hastalar")]
        public string HastalarOutput { get; set; }

        [DisplayName("Hastalar")]
        public List<int> HastaIdleriInput { get; set; }
        #endregion
    }
}
