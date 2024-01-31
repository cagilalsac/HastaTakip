#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class BransModel : Record
    {
        #region Entity Özellikleri
        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "{0} minimum {2} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; }
        #endregion

        #region Ekstra Özellikler
        [DisplayName("Doktorlar")]
        public string DoktorlarOutput { get; set; }

        [DisplayName("Doktorlar")]
        [Required(ErrorMessage = "{0} zorunludur!")]
        public List<int> DoktorlarInput { get; set; }
        #endregion
    }
}
