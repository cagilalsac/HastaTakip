//#nullable disable kullanmadan model oluşturma:

using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class UlkeModel : Record
	{
		#region Entity Özellikleri
		[StringLength(75, MinimumLength = 3, ErrorMessage = "{0} en az {2} en çok {1} karakter olmalıdır!")]
		[DisplayName("Ülke Adı")]
		public string Adi { get; set; } = null!;
		#endregion
	}
}
