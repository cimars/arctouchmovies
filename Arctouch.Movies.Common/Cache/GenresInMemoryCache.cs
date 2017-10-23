using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Interfaces.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arctouch.Movies.Common.Cache
{
	/// <summary>
	/// Holds in memory the list of genres available on themoviedb
	/// </summary>
	public class GenresInMemoryCache
	{
		#region Fields
		private IList<Genre> _genres;
		private IGenreManager _genreManager;
		#endregion

		#region Constructors
		public GenresInMemoryCache(IGenreManager genreManager)
		{
			_genreManager = genreManager;
		}
		#endregion

		#region Methods
		public async Task<IList<Genre>> GetGenres()
		{
			if (_genres == null)
			{
				_genres = await _genreManager.GetAllGenres();
			}

			return _genres;
		}
		#endregion
	}
}