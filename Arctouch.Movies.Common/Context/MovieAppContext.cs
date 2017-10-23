using Arctouch.Movies.Common.Cache;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace Arctouch.Movies.Common.Context
{
	/// <summary>
	/// Holds global configuration for the application
	/// </summary>
	public static class MovieAppContext
	{
		#region Fields
		private static ApiConfig _currentApiConfig;
		private static IUnityContainer _unityContainer;
		private static GenresInMemoryCache _genresInMemoryCache;
		#endregion

		#region Properties
		/// <summary>
		/// Dependencey Injection Container to get implementations for the application
		/// </summary>
		public static IUnityContainer DIContainer
		{
			get
			{
				if (_unityContainer == null)
				{
					throw new ArctouchMovieException("No DI Container configured.");
				}

				return _unityContainer;
			}
			set
			{
				_unityContainer = value;
			}
		}

		public static ApiConfig CurrentApiConfig
		{
			get
			{
				if (_currentApiConfig == null)
				{
					throw new ArctouchMovieException("No current api config was set.");
				}

				return _currentApiConfig;
			}
			set
			{
				_currentApiConfig = value;
			}
		}

		public static GenresInMemoryCache GenresCache
		{
			get
			{
				if (_genresInMemoryCache == null)
				{
					_genresInMemoryCache = new GenresInMemoryCache(DIContainer.Resolve<IGenreManager>());
				}

				return _genresInMemoryCache;
			}
			set
			{
				_genresInMemoryCache = value;
			}
		}
		#endregion
	}
}