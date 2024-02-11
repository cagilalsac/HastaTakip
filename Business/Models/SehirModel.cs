//#nullable disable kullanmadan model oluşturma:

using DataAccess.Records.Bases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
	public class SehirModel : Record
	{
		#region Entity Özellikleri
		[StringLength(125, MinimumLength = 3, ErrorMessage = "{0} en az {2} en çok {1} karakter olmalıdır!")]
		[DisplayName("Şehir Adı")]
		public string Adi { get; set; } = null!;

		public int UlkeId { get; set; }
		#endregion
	}
}
