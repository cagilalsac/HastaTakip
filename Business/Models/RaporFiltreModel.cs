#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class RaporFiltreModel
    {
        [DisplayName("Doktor Adı Soyadı")]
        public string DoktorAdiSoyadi { get; set; }

        [DisplayName("Doktor Kliniği")]
        public int? DoktorKlinikId { get; set; }

        [DisplayName("Hasta Adı Soyadı")]
        public string HastaAdiSoyadi { get; set; }

        [DisplayName("Hasta Doğum Tarihi")]
        public DateTime? HastaDogumTarihiBaslangic { get; set; }

        public DateTime? HastaDogumTarihiBitis { get; set; }
    }
}
