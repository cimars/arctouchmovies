using Microsoft.Practices.Unity;
using Arctouch.Movies.Common.Interfaces.Services;
using Arctouch.Movies.Services;
using Arctouch.Movies.Common.Interfaces.Managers;
using Arctouch.Movies.Managers;
using Arctouch.Movies.Common.Context;

namespace Arctouch.Movies.Config
{
	/// <summary>
	/// Centralized class to configure Dependency Injection Container for Unity
	/// </summary>
	public static class DIConfigurator
	{
		/// <summary>
		/// Configure the Unity DI Container
		/// </summary>
		/// <param name="container">Unity DI Container to configure</param>
		public static void Configure(IUnityContainer container)
		{
			container.RegisterType<IGenreService, GenreServiceClient>(new InjectionConstructor(ApiContext.BaseApiUrl, ApiContext.ApiKey, ApiContext.Language));
			container.RegisterType<IApiConfigService, ApiConfigServiceClient>(new InjectionConstructor(ApiContext.BaseApiUrl, ApiContext.ApiKey, ApiContext.Language));
			container.RegisterType<IMovieService, MoviesServiceClient>(new InjectionConstructor(ApiContext.BaseApiUrl, ApiContext.ApiKey, ApiContext.Language));

			container.RegisterType<IGenreManager, GenreManager>();
			container.RegisterType<IConfigManager, ConfigManager>();
			container.RegisterType<IMovieManager, MovieManager>();
		}
	}
}
