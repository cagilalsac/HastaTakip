using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IRaporService
	{
		IQueryable<RaporModel> Query(bool useInnerJoin = false);
    }

	public class RaporService : IRaporService
	{
		private readonly Db _db;

		public RaporService(Db db)
		{
			_db = db;
		}

        public IQueryable<RaporModel> Query(bool useInnerJoin = false)
		{
			IQueryable<RaporModel> query = null;

			IQueryable<Doktor> doktorQuery = _db.Doktorlar;
			IQueryable<Hasta> hastaQuery = _db.Hastalar;
			IQueryable<DoktorHasta> doktorHastaQuery = _db.DoktorHastalar;
			IQueryable<Klinik> klinikQuery = _db.Klinikler;
			IQueryable<Brans> bransQuery = _db.Branslar;
			IQueryable<Ulke> ulkeQuery = _db.Ulkeler;
			IQueryable<Sehir> sehirQuery = _db.Sehirler;

			if (useInnerJoin)
			{
				query = from doktorlar in doktorQuery
						join klinikler in klinikQuery
						on doktorlar.KlinikId equals klinikler.Id
						join branslar in bransQuery
						on doktorlar.BransId equals branslar.Id
						join ulkeler in ulkeQuery
						on doktorlar.UlkeId equals ulkeler.Id
						join sehirler in sehirQuery
						on doktorlar.SehirId equals sehirler.Id
						join doktorHastalar in doktorHastaQuery
						on doktorlar.Id equals doktorHastalar.DoktorId
						join hastalar in hastaQuery
						on doktorHastalar.HastaId equals hastalar.Id

						// Entity üzerinden Order By 1. yöntem:
						orderby doktorlar.UzmanMi descending, ulkeler.Adi, sehirler.Adi, klinikler.Adi, branslar.Adi, doktorlar.Adi, doktorlar.Soyadi

						// Entity üzerinden Where: Örnek kullanım
						//where ulkeler.Adi == "Türkiye" // &&, || sehirler.Adi == "Ankara" ...

						select new RaporModel()
						{
							DoktorAdiSoyadi = doktorlar.Adi + " " + doktorlar.Soyadi + (doktorlar.UzmanMi ? " (Uzman)" : ""),
							DoktorLokasyonu = "Ülke: " + ulkeler.Adi + ", Şehir: " + sehirler.Adi,
							DoktorKlinigi = klinikler.Adi,
							DoktorBransi = branslar.Adi,
							HastaAdiSoyadi = hastalar.Adi + " " + hastalar.Soyadi,
							HastaCinsiyeti = hastalar.Cinsiyeti.ToString(),
							HastaDogumTarihi = hastalar.DogumTarihi.ToShortDateString() // ToString("dd.MM.yyyy")
						};
			}
			return query;
		}
	}
}
