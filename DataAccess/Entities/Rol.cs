#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Rol : Record
	{
		[Required]
		[StringLength(10)]
        public string Adi { get; set; }

        public List<Kullanici> Kullanicilar { get; set; }
    }
}
