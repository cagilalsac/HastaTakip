#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class RaporModel
	{
        [DisplayName("Doktor Adı Soyadı")]
        public string DoktorAdiSoyadi { get; set; } // (Uzman veya Uzman Değil)

		[DisplayName("Doktor Lokasyonu")]
		public string DoktorLokasyonu { get; set; } // Ülke (Şehir)

		[DisplayName("Doktor Kliniği")]
		public string DoktorKlinigi { get; set; }

		[DisplayName("Doktor Branşı")]
		public string DoktorBransi { get; set; }

		[DisplayName("Hasta Adı Soyadı")]
		public string HastaAdiSoyadi { get; set; }

		[DisplayName("Hasta Doğum Tarihi")]
		public string HastaDogumTarihi { get; set; }

		[DisplayName("Hasta Cinsiyeti")]
		public string HastaCinsiyeti { get; set; }



        #region Filtreleme Özellikleri
        [DisplayName("Ülke")]
        public int? UlkeId { get; set; }

        [DisplayName("Şehir")]
        public int? SehirId { get; set; }

        [DisplayName("Klinik")]
        public int? KlinikId { get; set; }

        [DisplayName("Branş")]
        public int? BransId { get; set; }
        #endregion
    }
}
