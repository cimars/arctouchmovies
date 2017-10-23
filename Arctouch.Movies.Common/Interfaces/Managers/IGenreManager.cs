using Arctouch.Movies.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arctouch.Movies.Common.Interfaces.Managers
{
	public interface IGenreManager
	{
		/// <summary>
		/// Get all genres available for movies
		/// </summary>
		/// <returns>Collection of available genres for movies</returns>
		Task<IList<Genre>> GetAllGenres();
	}
}