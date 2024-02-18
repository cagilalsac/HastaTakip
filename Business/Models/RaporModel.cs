#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class RaporModel // left outer join kullanılıyorsa sorguda sol yani en üst tarafta kullanılan entity dışındaki
						    // tüm entity ile ilişkili model özellik tipleri nullable tanımlanmalıdır!
	{
        [DisplayName("Doktor Adı Soyadı")]
        public string DoktorAdiSoyadi { get; set; } // (Uzman veya Uzman Değil), Doktor entity'si üzerinden model özelliği

        [DisplayName("Doktor Lokasyonu")]
		public string DoktorLokasyonu { get; set; } // Ülke (Şehir), Hem Ülke hem de Sehir entity'leri üzerinden model özelliği

        [DisplayName("Doktor Kliniği")]
		public string DoktorKlinigi { get; set; } // Klinik entity'si üzerinden model özelliği

        [DisplayName("Doktor Branşı")]
		public string DoktorBransi { get; set; } // Brans entity'si üzerinden model özelliği

        [DisplayName("Hasta Adı Soyadı")]
		public string HastaAdiSoyadi { get; set; } // Hasta entity'si üzerinden model özelliği

        [DisplayName("Hasta Doğum Tarihi")]
		public string HastaDogumTarihi { get; set; } // Hasta entity'si üzerinden model özelliği

        [DisplayName("Hasta Cinsiyeti")]
		public string HastaCinsiyeti { get; set; } // Hasta entity'si üzerinden model özelliği



        #region Filtreleme Özellikleri
        public int? KlinikIdInput { get; set; } // Klinik entity'si üzerinden model özelliği

        public DateTime? HastaDogumTarihiInput { get; set; } // Hasta entity'si üzerinden model özelliği 
        #endregion
    }
}
