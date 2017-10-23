using Arctouch.Movies.Common.Entities;
using System.Threading.Tasks;

namespace Arctouch.Movies.Common.Interfaces.Managers
{
	public interface IMovieManager
	{
		/// <summary>
		/// Get upcoming movies
		/// </summary>
		/// <param name="page">Number of page</param>
		/// <returns>Movies and other related data to the number of page requested</returns>
		Task<MoviePage> GetUpcomingMovies(int page);

		/// <summary>
		/// Get movie details
		/// </summary>
		/// <param name="id">The movie id to get details</param>
		/// <returns>Movie details</returns>
		Task<Movie> GetMovieById(int id);

		/// <summary>
		/// Search for movies
		/// </summary>
		/// <param name="query">Text query to search</param>
		/// <param name="page">Specify which page to query</param>
		/// <returns>Movies that match query text and other related data to the number of page requeste</returns>
		Task<MoviePage> Search(string query, int page);
	}
}