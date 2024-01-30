using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IKlinikService
    {
        IQueryable<KlinikModel> Query();
        Result Add(KlinikModel model);
        Result Update(KlinikModel model);
        Result Delete(int id);

        // extra
        KlinikModel GetItem(int id);
        List<KlinikModel> GetList();
    }

    public class KlinikService : IKlinikService
    {
        private readonly Db _db;

        public KlinikService(Db db)
        {
            _db = db;
        }

        public IQueryable<KlinikModel> Query()
        {
            return _db.Klinikler.OrderBy(k => k.Adi).Select(k => new KlinikModel() // k: Klinikler'deki her bir Klinik tipindeki eleman delegesi
            {
                // entity özellikleri
                Aciklamasi = k.Aciklamasi,
                Adi = k.Adi,
                Guid = k.Guid,
                Id = k.Id,

                // ekstra özellikler
                DoktorSayisiOutput = k.Doktorlar.Count,
                DoktorlarOutput = string.Join(", ", k.Doktorlar.Select(doktor => doktor.Adi + " " + doktor.Soyadi))
            }); 
        }

        // 1. yöntem: Entity Framework Eager Loading
        //public KlinikModel GetItem(int id)
        //{
        //    // 1. yöntem:
        //    //Klinik entity = _db.Klinikler.Find(id);
        //    // 2. yöntem:
        //    //Klinik entity = _db.Klinikler.Include("Doktorlar").SingleOrDefault(k => k.Id == id);
        //    Klinik entity = _db.Klinikler.Include(k => k.Doktorlar).SingleOrDefault(k => k.Id == id);

        //    if (entity is null)
        //        return null;

        //    // 1. yöntem:
        //    //string doktorlar = string.Empty; // ""
        //    //foreach (Doktor doktor in entity.Doktorlar)
        //    //{
        //    //    doktorlar += doktor.Adi + " " + doktor.Soyadi + "<br />";
        //    //}
        //    //doktorlar = doktorlar.TrimEnd("<br />".ToCharArray());
        //    // 2. yöntem:
        //    string doktorlar = string.Join("<br />", entity.Doktorlar.Select(doktor => doktor.Adi + " " + doktor.Soyadi));

        //    KlinikModel model = new KlinikModel()
        //    {
        //        // entity özellikleri
        //        Aciklamasi = entity.Aciklamasi,
        //        Adi = entity.Adi,
        //        Guid = entity.Guid,
        //        Id = entity.Id,

        //        // ekstra özellikler
        //        DoktorSayisiOutput = entity.Doktorlar.Count,
        //        DoktorlarOutput = doktorlar
        //    };
        //    return model;
        //}
        // 2. yöntem:
        public KlinikModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);

        public List<KlinikModel> GetList() => Query().ToList();

        public Result Add(KlinikModel model)
        {
            // 1. yöntem:
            //Klinik mevcutKlinik = _db.Klinikler.SingleOrDefault(k => k.Adi.ToUpper() == model.Adi.ToUpper().Trim()); // case insensitive
            //if (mevcutKlinik is not null)
            //    return new ErrorResult("Girilen klinik adına sahip klinik bulunmaktadır!");
            // 2. yöntem:
            if (_db.Klinikler.Any(k => k.Adi.ToLower() == model.Adi.ToLower().Trim()))
                return new ErrorResult("Girilen klinik adına sahip klinik bulunmaktadır!");

            Klinik entity = new Klinik()
            {
                Aciklamasi = model.Aciklamasi?.Trim(),
                Adi = model.Adi.Trim(),
                Guid = Guid.NewGuid().ToString()
            };

            // 1. yöntem:
            //_db.Klinikler.Add(entity);
            // 2. yöntem:
            _db.Add(entity);

            _db.SaveChanges();

            model.Id = entity.Id;

            return new SuccessResult("Klinik başarıyla eklendi.");
        }

        public Result Update(KlinikModel model)
        {
            if (_db.Klinikler.Any(k => k.Id != model.Id && k.Adi.ToLower() == model.Adi.ToLower()))
                return new ErrorResult("Girilen klinik adına sahip klinik bulunmaktadır!");
            Klinik mevcutKlinik = _db.Klinikler.SingleOrDefault(k => k.Id == model.Id);
            if (mevcutKlinik is null)
                return new ErrorResult("Klinik bulunamadı!");
            mevcutKlinik.Aciklamasi = model.Aciklamasi?.Trim();
            mevcutKlinik.Adi = model.Adi.Trim();

            // 1. yöntem:
            //_db.Klinikler.Update(mevcutKlinik);
            // 2. yöntem:
            _db.Update(mevcutKlinik);

            _db.SaveChanges();

            return new SuccessResult("Klinik başarıyla güncellendi.");
        }

        public Result Delete(int id)
        {
            Klinik mevcutKlinik = _db.Klinikler.Include(k => k.Doktorlar).SingleOrDefault(k => k.Id == id);
            if (mevcutKlinik == null)
                return new ErrorResult("Klinik bulunamadı!");

            // 1. yöntem:
            //if (mevcutKlinik.Doktorlar.Count > 0)
            // 2. yöntem:
            if (mevcutKlinik.Doktorlar.Any())
                return new ErrorResult("Kliniğin ilişkili doktorları bulunmaktadır!");

            // 1. yöntem:
            //_db.Klinikler.Remove(mevcutKlinik);
            // 2. yöntem:
            _db.Remove(mevcutKlinik);

            _db.SaveChanges();

            return new SuccessResult("Klinik başarıyla silindi.");
        }
    }
}
