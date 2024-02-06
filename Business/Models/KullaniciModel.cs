#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class KullaniciModel : Record
	{
		#region Entity Özellikleri
		[Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} minimum {2} maksimum {1} karakter olmalıdır!")]
		[DisplayName("Kullanıcı Adı")]
		public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} minimum {2} maksimum {1} karakter olmalıdır!")]
        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "{0} minimum {2} maksimum {1} karakter olmalıdır!")]
        [Compare("Sifre", ErrorMessage = "{0} ile {1} aynı olmalıdır!")]
        [DisplayName("Şifre Onay")]
        public string SifreOnay { get; set; }

        public bool AktifMi { get; set; }

		public int RolId { get; set; }
        #endregion

        #region Ekstra Özellikler
        public RolModel RolOutput { get; set; }
        #endregion
    }
}
