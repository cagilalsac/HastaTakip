using Business.Models;
using DataAccess.Contexts;

namespace Business.Services
{
	public abstract class SehirServiceBase
	{
		protected readonly Db _db;

		protected SehirServiceBase(Db db)
		{
			_db = db;
		}

		public virtual IQueryable<SehirModel> Query()
		{
			return _db.Sehirler.OrderBy(s => s.Adi).Select(s => new SehirModel()
			{
				Adi = s.Adi,
				Guid = s.Guid,
				Id = s.Id,
				UlkeId = s.UlkeId,
			});
		}

		public virtual List<SehirModel> GetList(int? ulkeId = null) => ulkeId is null ? Query().ToList() : Query().Where(s => s.UlkeId == ulkeId).ToList();

		public virtual SehirModel GetItem(int id) => Query().SingleOrDefault(q => q.Id == id);
	}

	public class SehirService : SehirServiceBase
	{
		public SehirService(Db db) : base(db)
		{
		}
	}
}
