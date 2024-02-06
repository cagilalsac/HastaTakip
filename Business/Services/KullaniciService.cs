using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
	public interface IKullaniciService
	{
		IQueryable<KullaniciModel> Query();
		Task<KullaniciModel> GetItemAsync(string kullaniciAdi, string sifre);
		Task<Result> AddAsync(KullaniciModel model);
	}

	public class KullaniciService : IKullaniciService
	{
		private readonly Db _db;

		public KullaniciService(Db db)
		{
			_db = db;
		}

		public IQueryable<KullaniciModel> Query()
		{
			return _db.Kullanicilar.OrderByDescending(k => k.AktifMi).ThenBy(k => k.KullaniciAdi).Select(k => new KullaniciModel()
			{
				AktifMi = k.AktifMi,
				Guid = k.Guid,
				Id = k.Id,
				KullaniciAdi = k.KullaniciAdi,
				Sifre = k.Sifre,
				RolId = k.RolId,
				
				RolOutput = new RolModel()
				{
					Adi = k.Rol.Adi,
					Guid = k.Rol.Guid,
					Id = k.Rol.Id
				}
			});
		}

		public async Task<KullaniciModel> GetItemAsync(string kullaniciAdi, string sifre)
		{
			return await Query().SingleOrDefaultAsync(k => k.KullaniciAdi == kullaniciAdi && k.Sifre == sifre && k.AktifMi);
		}

		public async Task<Result> AddAsync(KullaniciModel model)
		{
			if (_db.Kullanicilar.Any(k => k.KullaniciAdi == model.KullaniciAdi.Trim()))
				return new ErrorResult("Girilen kullanıcı adına sahip kullanıcı bulunmaktadır!");

			var entity = new Kullanici()
			{
				AktifMi = true,
				Guid = Guid.NewGuid().ToString(),
				KullaniciAdi = model.KullaniciAdi.Trim(),
				Sifre = model.Sifre.Trim(),
				RolId = (int)Roller.Kullanici
			};

			_db.Kullanicilar.Add(entity);
			await _db.SaveChangesAsync();

			return new SuccessResult("Kullanıcı başarıyla eklendi.");
		}
	}
}
