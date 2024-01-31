using Business.Models;
using DataAccess.Contexts;

namespace Business.Services
{
    public interface IDoktorService
    {
        IQueryable<DoktorModel> Query();
    }

    public class DoktorService : IDoktorService
    {
        private readonly Db _db;

        public DoktorService(Db db)
        {
            _db = db;
        }

        public IQueryable<DoktorModel> Query()
        {
            return _db.Doktorlar.OrderBy(d => d.Adi).ThenBy(d => d.Soyadi).Select(d => new DoktorModel()
            {
                // entity özellikleri
                Adi = d.Adi,
                BransId = d.BransId,
                Guid = d.Guid,
                Id = d.Id,
                KlinikId = d.KlinikId,
                Soyadi = d.Soyadi,
                UzmanMi = d.UzmanMi,

                // ekstra özellikler
                AdiSoyadiOutput = d.Adi + " " + d.Soyadi
            });
        }
    }
}
