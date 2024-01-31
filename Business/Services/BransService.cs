using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IBransService
    {
        IQueryable<BransModel> Query();
        Result Add(BransModel model);
        Result Update(BransModel model);
        Result Delete(int id);
    }

    public class BransService : IBransService
    {
        private readonly Db _db;

        public BransService(Db db)
        {
            _db = db;
        }

        public IQueryable<BransModel> Query()
        {
            return _db.Branslar.Include(b => b.Doktorlar).OrderByDescending(b => b.Doktorlar.Count).ThenBy(b => b.Adi)
                .Select(b => new BransModel()
                {
                    // entity özellikleri
                    Adi = b.Adi,
                    Guid = b.Guid,
                    Id = b.Id,

                    // ekstra özellikler
                    DoktorlarOutput = string.Join("<br />", b.Doktorlar.OrderBy(b => b.Adi).ThenBy(b => b.Soyadi).Select(b => b.Adi + " " + b.Soyadi)),
                    DoktorlarInput = b.Doktorlar.Select(d => d.Id).ToList()
                });
        }

        public Result Add(BransModel model)
        {
            if (_db.Branslar.Any(b => b.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girilen branş adına sahip branş bulunmaktadır!");

            var entity = new Brans()
            {
                // entity özellikleri
                Adi = model.Adi.Trim(),
                Guid = Guid.NewGuid().ToString(),

                // entity ilişki özellikleri
                Doktorlar = _db.Doktorlar.Where(d => model.DoktorlarInput.Contains(d.Id)).ToList()
            };

            _db.Branslar.Add(entity);
            _db.SaveChanges();

            return new SuccessResult("Branş başarıyla eklendi.");
        }

        public Result Update(BransModel model)
        {
            if (_db.Branslar.Any(b => b.Adi.ToLower() == model.Adi.ToLower().Trim() && b.Id != model.Id))
                return new ErrorResult("Girilen branş adına sahip branş bulunmaktadır!");

            var entity = _db.Branslar.SingleOrDefault(b => b.Id == model.Id);
            if (entity is null)
                return new ErrorResult("Branş bulunamadı!");

            // entity özellikleri
            entity.Adi = model.Adi.Trim();

            // entity ilişki özellikleri
            entity.Doktorlar = _db.Doktorlar.Where(d => model.DoktorlarInput.Contains(d.Id)).ToList();

            _db.Branslar.Update(entity);
            _db.SaveChanges();

            return new SuccessResult("Branş başarıyla güncellendi.");
        }

        public Result Delete(int id)
        {
            var entity = _db.Branslar.Include(b => b.Doktorlar).SingleOrDefault(b => b.Id == id);
            if (entity is null)
                return new ErrorResult("Branş bulunamadı!");

            if (entity.Doktorlar.Any())
                return new ErrorResult("Branşın ilişkili doktorları bulunmaktadır!");

            _db.Branslar.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Branş başarıyla silindi.");
        }
    }
}
