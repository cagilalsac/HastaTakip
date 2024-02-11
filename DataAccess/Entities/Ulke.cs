//#nullable disable kullanmadan entity oluşturma:

using DataAccess.Records.Bases;

namespace DataAccess.Entities
{
    public class Ulke : Record
    {
        public string Adi { get; set; } = null!;

        public List<Sehir>? Sehirler { get; set; }

        public List<Doktor>? Doktorlar { get; set; }
    }
}
