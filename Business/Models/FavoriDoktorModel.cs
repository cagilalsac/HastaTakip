#nullable disable

using System.ComponentModel;

namespace Business.Models
{
    public class FavoriDoktorModel
    {
        public int HastaId { get; set; }
        public int DoktorId { get; set; }

        [DisplayName("Doktor")]
        public string DoktorAdiSoyadi { get; set; }
    }
}
