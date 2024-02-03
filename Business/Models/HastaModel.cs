#nullable disable

using DataAccess.Enums;
using DataAccess.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
	public class HastaModel : Record
	{
        #region Entity Özellikleri
        [Required(ErrorMessage = "{0} zorunludur!")]
        [MaxLength(50, ErrorMessage = "{0} en çok {1} karakter olmalıdır!")]
        [MinLength(2, ErrorMessage = "{0} en az {1} karakter olmalıdır!")]
        [DisplayName("Adı")]
        public string Adi { get; set; } // Çağıl

        [Required(ErrorMessage = "{0} zorunludur!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} en az {2} en çok {1} karakter olmalıdır!")]
        [DisplayName("Soyadı")]
        public string Soyadi { get; set; } // Alsaç

		[StringLength(11, MinimumLength = 11, ErrorMessage = "{0} {1} karakter olmalıdır!")]
        [DisplayName("T.C. Kimlik No")]
        public string KimlikNo { get; set; }

		[DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "{0} zorunludur!")]
        public DateTime? DogumTarihi { get; set; } // 01.02.2024 00:00:00

		public Cinsiyetler Cinsiyeti { get; set; }

        [Range(0.3, 3, ErrorMessage = "{0} {1} ile {2} metre aralığında olmalıdır!")]
		public decimal? Boyu { get; set; } // 1.75

        [Range(1, double.MaxValue, ErrorMessage = "{0} {1} kilodan büyük olmalıdır!")]
        public double? Kilosu { get; set; } // 52.5
        #endregion

        #region Ekstra Özellikler
        [DisplayName("Tam Adı")]
        public string AdiSoyadiOutput { get; set; } // Çağıl Alsaç

		[DisplayName("Tam Adı Test 1")] // test amaçlı, serviste atanmalı
		public string AdiSoyadiTestOutput => Adi + " " + Soyadi;

		[DisplayName("Doğum Tarihi")]
        public string DogumTarihiOutput { get; set; } // 01.02.2024

		[DisplayName("Boyu")]
        public string BoyuOutput { get; set; } // 1,75 m

        [DisplayName("Kilosu")]
        public string KilosuOutput { get; set; } // 52,5 kg

        [DisplayName("Doktorlar")]
        public string DoktorlarOutput { get; set; }

        [DisplayName("Doktorlar")]
        //[Required(ErrorMessage = "{0} zorunludur!")] // hastanın en az 1 doktoru olmalı
        public List<int> DoktorIdleriInput { get; set; }
        #endregion
    }
}
