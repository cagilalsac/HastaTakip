#nullable disable

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class RolModel : Record
	{
		#region Entity Özellikleri
		[Required]
		[StringLength(10)]
		public string Adi { get; set; }
		#endregion

		#region Ekstra Özellikler
		public List<KullaniciModel> KullanicilarOutput { get; set; }
		#endregion
	}
}