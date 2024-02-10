using Business.Models;
using Newtonsoft.Json;

namespace MVC.Utilities.Bases
{
	public abstract class FavoriDoktorlarSessionUtilBase
	{
		protected readonly string _sessionKey = "FavoriDoktorlar";

		protected readonly IHttpContextAccessor _httpContextAccessor;

		protected FavoriDoktorlarSessionUtilBase(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public virtual List<FavoriDoktorModel> GetSession()
		{
			List<FavoriDoktorModel> list = new List<FavoriDoktorModel>();
			string json = _httpContextAccessor.HttpContext.Session.GetString(_sessionKey);
			if (!string.IsNullOrWhiteSpace(json))
			{
				// JSON string -> C# object: Deserialize
				list = JsonConvert.DeserializeObject<List<FavoriDoktorModel>>(json);
			}
			return list;
		}

		public virtual void SetSession(List<FavoriDoktorModel> list)
		{
			// C# object -> JSON string: Serialize
			string json = JsonConvert.SerializeObject(list);
			_httpContextAccessor.HttpContext.Session.SetString(_sessionKey, json);
		}
	}
}
