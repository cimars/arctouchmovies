using Arctouch.Movies.Common.Entities;
using System.Threading.Tasks;

namespace Arctouch.Movies.Common.Interfaces.Services
{
	public interface IApiConfigService
	{
		/// <summary>
		/// Get the api configuration from Movie web api
		/// </summary>
		/// <returns>Api configuration data</returns>
		Task<ApiConfig> GetApiConfig();
	}
}