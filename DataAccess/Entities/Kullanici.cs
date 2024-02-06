#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
	public class Kullanici : Record
	{
		[Required]
		[StringLength(15)]
        public string KullaniciAdi { get; set; }

		[Required]
		[StringLength(10)]
		public string Sifre { get; set; }

        public bool AktifMi { get; set; }

        public int RolId { get; set; }

        public Rol Rol { get; set; }
    }
}
