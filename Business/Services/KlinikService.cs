using Business.Models;
using DataAccess.Contexts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public interface IKlinikService
    {
        IQueryable<KlinikModel> Query();

        KlinikModel GetItem(int id);
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

        public KlinikModel GetItem(int id)
        {
            // 1. yöntem:
            //Klinik entity = _db.Klinikler.Find(id);
            // 2. yöntem:
            //Klinik entity = _db.Klinikler.Include("Doktorlar").SingleOrDefault(k => k.Id == id);
            Klinik entity = _db.Klinikler.Include(k => k.Doktorlar).SingleOrDefault(k => k.Id == id);

            if (entity is null)
                return null;

            // 1. yöntem:
            //string doktorlar = string.Empty; // ""
            //foreach (Doktor doktor in entity.Doktorlar)
            //{
            //    doktorlar += doktor.Adi + " " + doktor.Soyadi + "<br />";
            //}
            //doktorlar = doktorlar.TrimEnd("<br />".ToCharArray());
            // 2. yöntem:
            string doktorlar = string.Join("<br />", entity.Doktorlar.Select(doktor => doktor.Adi + " " + doktor.Soyadi));

            KlinikModel model = new KlinikModel()
            {
                // entity özellikleri
                Aciklamasi = entity.Aciklamasi,
                Adi = entity.Adi,
                Guid = entity.Guid,
                Id = entity.Id,

                // ekstra özellikler
                DoktorSayisiOutput = entity.Doktorlar.Count,
                DoktorlarOutput = doktorlar
            };
            return model;
        }
    }
}
