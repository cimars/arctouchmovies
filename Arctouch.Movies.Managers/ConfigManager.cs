using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Interfaces.Managers;
using Arctouch.Movies.Common.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Arctouch.Movies.Managers
{
	public class ConfigManager : IConfigManager
	{
		private readonly IApiConfigService _configServiceClient;

		public ConfigManager(IApiConfigService configServiceClient)
		{
			_configServiceClient = configServiceClient;
		}

		public async Task<ApiConfig> GetApiConfig()
		{
			try
			{
				// Here can come additional logic to validate, configure, cache, etc the Api Config retrieved from web api
				return await _configServiceClient.GetApiConfig();
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieManagerException("An unexpected error happend trying to get api config.", e);
			}
		}
	}
}
