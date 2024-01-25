using Business.Models;
using DataAccess.Contexts;

namespace Business.Services
{
    public interface IKlinikService
    {
        IQueryable<KlinikModel> Query();
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
                Aciklamasi = k.Aciklamasi,
                Adi = k.Adi,
                Guid = k.Guid,
                Id = k.Id
            }); 
        }
    }
}
