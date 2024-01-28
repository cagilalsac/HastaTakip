#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class KlinikModel : Record
    {
        #region Entity Özellikleri
        [Required]
        [StringLength(200)]
        [DisplayName("Adı")]
        public string Adi { get; set; }

        [DisplayName("Açıklaması")]
        public string Aciklamasi { get; set; }
        #endregion

        #region Ekstra Özellikler
        [DisplayName("Doktor Sayısı")]
        public int DoktorSayisiOutput { get; set; }

        [DisplayName("Doktorlar")]
        public string DoktorlarOutput { get; set; }
        #endregion
    }
}
