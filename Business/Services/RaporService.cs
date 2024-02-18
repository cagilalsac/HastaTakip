using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Business.Services
{
	public interface IRaporService
	{
		IQueryable<RaporModel> Query();
		List<RaporModel> GetList(RaporFiltreModel filtre = null);

        IQueryable<RaporModel> QueryLeftOuterJoin();
    }

	public class RaporService : IRaporService
	{
		private readonly Db _db;

		public RaporService(Db db)
		{
			_db = db;
		}

        public IQueryable<RaporModel> Query() // inner join
		{
			IQueryable<RaporModel> query;

			IQueryable<Doktor> doktorQuery = _db.Doktorlar;
			IQueryable<Hasta> hastaQuery = _db.Hastalar;
			IQueryable<DoktorHasta> doktorHastaQuery = _db.DoktorHastalar;
			IQueryable<Klinik> klinikQuery = _db.Klinikler;
			IQueryable<Brans> bransQuery = _db.Branslar;
			IQueryable<Ulke> ulkeQuery = _db.Ulkeler;
			IQueryable<Sehir> sehirQuery = _db.Sehirler;
			
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

                    // Order By Entity üzerinden 1. örnek:
                    //orderby doktorlar.UzmanMi descending, ulkeler.Adi, sehirler.Adi, klinikler.Adi, branslar.Adi, doktorlar.Adi, doktorlar.Soyadi
                    // 2. örnek 1. yöntem:
                    //orderby klinikler.Adi, branslar.Adi, doktorlar.Adi, doktorlar.Soyadi

                    // Entity üzerinden Where: Örnek kullanım
                    //where ulkeler.Adi == "Türkiye" // &&, || sehirler.Adi == "Ankara" ...

                    select new RaporModel()
					{
						DoktorAdiSoyadi = doktorlar.Adi + " " + doktorlar.Soyadi + (doktorlar.UzmanMi ? " (Uzman)" : ""),
						DoktorLokasyonu = "Ülke: " + ulkeler.Adi + "<br />Şehir: " + sehirler.Adi,
						DoktorKlinigi = klinikler.Adi,
						DoktorBransi = branslar.Adi,
						HastaAdiSoyadi = hastalar.Adi + " " + hastalar.Soyadi,
						HastaCinsiyeti = hastalar.Cinsiyeti.ToString(),
						HastaDogumTarihi = hastalar.DogumTarihi.ToShortDateString(), // ToString("dd.MM.yyyy")

						// filtreleme özellikleri
						KlinikIdInput = klinikler.Id,
						HastaDogumTarihiInput = hastalar.DogumTarihi
					};

			// Order By Model üzerinden 2. örnek 2. yöntem:
			query = query.OrderBy(q => q.DoktorKlinigi).ThenBy(q => q.DoktorBransi).ThenBy(q => q.DoktorAdiSoyadi);

			return query;
		}

        public IQueryable<RaporModel> QueryLeftOuterJoin()
        {
            IQueryable<RaporModel> query;

            IQueryable<Doktor> doktorQuery = _db.Doktorlar;
            IQueryable<Hasta> hastaQuery = _db.Hastalar;
            IQueryable<DoktorHasta> doktorHastaQuery = _db.DoktorHastalar;
            IQueryable<Klinik> klinikQuery = _db.Klinikler;
            IQueryable<Brans> bransQuery = _db.Branslar;
            IQueryable<Ulke> ulkeQuery = _db.Ulkeler;
            IQueryable<Sehir> sehirQuery = _db.Sehirler;

			query = from doktorlar in doktorQuery
					join klinikler in klinikQuery
					on doktorlar.KlinikId equals klinikler.Id into klinikJoin
					from klinik in klinikJoin.DefaultIfEmpty()
					join branslar in bransQuery
					on doktorlar.BransId equals branslar.Id into bransJoin
					from brans in bransJoin.DefaultIfEmpty()
					join ulkeler in ulkeQuery
					on doktorlar.UlkeId equals ulkeler.Id into ulkeJoin
					from ulke in ulkeJoin.DefaultIfEmpty()
					join sehirler in sehirQuery
					on doktorlar.SehirId equals sehirler.Id into sehirJoin
					from sehir in sehirJoin.DefaultIfEmpty()
					join doktorHastalar in doktorHastaQuery
					on doktorlar.Id equals doktorHastalar.DoktorId into doktorHastaJoin
					from doktorHasta in doktorHastaJoin.DefaultIfEmpty()
					join hastalar in hastaQuery
					on doktorHasta.HastaId equals hastalar.Id into hastaJoin
					from hasta in hastaJoin.DefaultIfEmpty()
					select new RaporModel()
					{
						DoktorAdiSoyadi = doktorlar.Adi + " " + doktorlar.Soyadi + (doktorlar.UzmanMi ? " (Uzman)" : ""),
						DoktorLokasyonu = "Ülke: " + ulke.Adi + "<br />Şehir: " + sehir.Adi,
						DoktorKlinigi = klinik.Adi,
						DoktorBransi = brans.Adi,
						HastaAdiSoyadi = hasta.Adi + " " + hasta.Soyadi,
						HastaCinsiyeti = hasta.Cinsiyeti.ToString(),
						HastaDogumTarihi = hasta.DogumTarihi.ToShortDateString(), // ToString("dd.MM.yyyy")

						// filtreleme özellikleri
						KlinikIdInput = klinik.Id,
						HastaDogumTarihiInput = hasta.DogumTarihi
					};

            query = query.OrderBy(q => q.DoktorKlinigi).ThenBy(q => q.DoktorBransi).ThenBy(q => q.DoktorAdiSoyadi);

			return query;
        }

        public List<RaporModel> GetList(RaporFiltreModel filtre = null)
        {
			//var query = Query(); // inner join
			var query = QueryLeftOuterJoin(); // left outer join

			if (filtre is not null)
			{
				if (!string.IsNullOrWhiteSpace(filtre.DoktorAdiSoyadi))
					query = query.Where(q => q.DoktorAdiSoyadi.ToLower().Contains(filtre.DoktorAdiSoyadi.ToLower().Trim()));
				if (filtre.DoktorKlinikId.HasValue)
					query = query.Where(q => q.KlinikIdInput == filtre.DoktorKlinikId.Value);
                if (!string.IsNullOrWhiteSpace(filtre.HastaAdiSoyadi))
                    query = query.Where(q => q.HastaAdiSoyadi.ToLower().Contains(filtre.HastaAdiSoyadi.ToLower().Trim()));
				if (filtre.HastaDogumTarihiBaslangic.HasValue)
					query = query.Where(q => q.HastaDogumTarihiInput >= filtre.HastaDogumTarihiBaslangic);
				if (filtre.HastaDogumTarihiBitis.HasValue)
					query = query.Where(q => q.HastaDogumTarihiInput <= filtre.HastaDogumTarihiBitis);
            }
			return query.ToList();
        }
    }
}
