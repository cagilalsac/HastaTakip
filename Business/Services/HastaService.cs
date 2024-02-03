using Business.Models;
using Business.Utilities.Bases;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Business.Services
{
	public interface IHastaService
	{
		IQueryable<HastaModel> Query();
		Result Add(HastaModel model);
		Result Update(HastaModel model);
		Result Delete(int id);
	}

	public class HastaService : IHastaService
	{
		private readonly Db _db;
		private readonly TcKimlikNoUtilBase _tcKimlikNoUtil;

        public HastaService(Db db, TcKimlikNoUtilBase tcKimlikNoUtil)
        {
            _db = db;
            _tcKimlikNoUtil = tcKimlikNoUtil;
        }

        public IQueryable<HastaModel> Query()
		{
			return _db.Hastalar.OrderByDescending(h => h.DogumTarihi).ThenBy(h => h.Adi).ThenBy(h => h.Soyadi)
				.Select(h => new HastaModel()
				{
					#region Entity Özellikleri
					Adi = h.Adi,
					Boyu = h.Boyu,
					Cinsiyeti = h.Cinsiyeti,
					DogumTarihi = h.DogumTarihi,
					Guid = h.Guid,
					Id = h.Id,
					Kilosu = h.Kilosu,
					KimlikNo = h.KimlikNo,
					Soyadi = h.Soyadi,
					#endregion

					#region Ekstra Özellikler
					AdiSoyadiOutput = h.Adi + " " + h.Soyadi,

					// Program.cs altında Localization region'ında uygulamanın bölgesel ayarını belirlediğimiz için
					// ondalık ve tarhi verisi formatlama işlemlerinde CultureInfo kullanmaya artık gerek kalmadı
					//BoyuOutput = (h.Boyu.HasValue ? h.Boyu.Value.ToString("N2", new CultureInfo("tr-TR")) : "0") + " metre",
					BoyuOutput = (h.Boyu.HasValue ? h.Boyu.Value.ToString("N2") : "0") + " metre",

					KilosuOutput = (h.Kilosu ?? 0).ToString("N2") + " kilogram",
					DogumTarihiOutput = h.DogumTarihi.ToString("dd.MM.yyyy"),
					DoktorlarOutput = string.Join(", ", h.DoktorHastalar.OrderBy(dh => dh.Doktor.Adi).ThenBy(dh => dh.Doktor.Soyadi)
						.Select(dh => dh.Doktor.Adi + " " + dh.Doktor.Soyadi + (dh.Doktor.UzmanMi ? " (Uzman)" : ""))),

					DoktorIdleriInput = h.DoktorHastalar.Select(dh => dh.DoktorId).ToList()
					#endregion
				});
		}

        public Result Add(HastaModel model)
        {
			if (_db.Hastalar.Any(h => h.Adi.ToLower() == model.Adi.ToLower().Trim() && h.Soyadi.ToLower() == model.Soyadi.ToLower().Trim()
				&& h.DogumTarihi == model.DogumTarihi))
				return new ErrorResult("Girilen hasta adı, soyadı ve doğum tarihine sahip hasta bulunmaktadır!");
			if (!string.IsNullOrWhiteSpace(model.KimlikNo))
			{
				var result = _tcKimlikNoUtil.Dogrula(model.KimlikNo.Trim());
				if (!result.IsSuccessful)
				{
					// 1. yöntem:
					//return new ErrorResult(result.Message);
					// 2. yöntem:
					return result; // error result
				}
			}

            Hasta entity = new Hasta()
			{
				Adi = model.Adi.Trim(),
				Boyu = model.Boyu,
				Cinsiyeti = model.Cinsiyeti,
				DogumTarihi = model.DogumTarihi.Value,
				Guid = Guid.NewGuid().ToString(),
				Kilosu = model.Kilosu,
				KimlikNo = model.KimlikNo,
				Soyadi = model.Soyadi.Trim(),

                // many to many ilişkili kayıtların oluşturulması
                DoktorHastalar = model.DoktorIdleriInput?.Select(doktorId => new DoktorHasta()
				{
					DoktorId = doktorId
				}).ToList()
			};

			_db.Hastalar.Add(entity);
			_db.SaveChanges();

			return new SuccessResult("Hasta başarıyla eklendi.");
        }

        public Result Update(HastaModel model)
        {
            if (_db.Hastalar.Any(h => h.Adi.ToLower() == model.Adi.ToLower().Trim() && h.Soyadi.ToLower() == model.Soyadi.ToLower().Trim()
                && h.DogumTarihi == model.DogumTarihi && h.Id != model.Id))
                return new ErrorResult("Girilen hasta adı, soyadı ve doğum tarihine sahip hasta bulunmaktadır!");
			if (!string.IsNullOrWhiteSpace(model.KimlikNo))
			{
				var result = _tcKimlikNoUtil.Dogrula(model.KimlikNo.Trim());
				if (!result.IsSuccessful)
					return result;
			}

			Hasta entity = _db.Hastalar.Include(h => h.DoktorHastalar).SingleOrDefault(h => h.Id == model.Id);
			if (entity is null)
				return new ErrorResult("Hasta bulunamadı!");

			// many to many ilişkili kayıtların silinmesi
			_db.DoktorHastalar.RemoveRange(entity.DoktorHastalar);

			entity.Adi = model.Adi.Trim();
			entity.Boyu = model.Boyu;
			entity.Cinsiyeti = model.Cinsiyeti;
			entity.DogumTarihi = model.DogumTarihi.Value;
			entity.Kilosu = model.Kilosu;
			entity.KimlikNo = model.KimlikNo;
			entity.Soyadi = model.Soyadi.Trim();

			// many to many ilişkili kayıtların oluşturulması
			entity.DoktorHastalar = model.DoktorIdleriInput?.Select(doktorId => new DoktorHasta()
			{
				DoktorId = doktorId
			}).ToList();

			_db.Hastalar.Update(entity);
			_db.SaveChanges();

            return new SuccessResult("Hasta başarıyla güncellendi.");
        }

        public Result Delete(int id)
        {
			Hasta entity = _db.Hastalar.Include(h => h.DoktorHastalar).SingleOrDefault(h => h.Id == id);

			if (entity is null)
				return new ErrorResult("Hasta bulunamadı!");

            // many to many ilişkili kayıtların silinmesi
            _db.DoktorHastalar.RemoveRange(entity.DoktorHastalar);

            _db.Hastalar.Remove(entity);
			_db.SaveChanges();

			return new SuccessResult("Hasta başarıyla silindi.");
        }
    }
}
