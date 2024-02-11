//#nullable disable kullanmadan entity oluşturma:

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class Sehir : Record
    {
        public string Adi { get; set; } = null!;

        public int UlkeId { get; set; }
        public Ulke? Ulke { get; set; }

        public List<Doktor>? Doktorlar { get; set; }
    }
}
