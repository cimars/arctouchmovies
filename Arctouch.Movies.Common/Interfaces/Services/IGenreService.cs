using Arctouch.Movies.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arctouch.Movies.Common.Interfaces.Services
{
    public interface IGenreService
    {
		/// <summary>
		/// Get all genres available for movies
		/// </summary>
		/// <returns>Collection of available genres for movies</returns>
		Task<IList<Genre>> GetAll();
    }
}
