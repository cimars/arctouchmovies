using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Interfaces.Managers;
using Arctouch.Movies.Common.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace Arctouch.Movies.Managers
{
	public class MovieManager : IMovieManager
	{
		private readonly IMovieService _movieServiceClient;

		public MovieManager(IMovieService movieServiceClient)
		{
			_movieServiceClient = movieServiceClient;
		}

		public async Task<Movie> GetMovieById(int id)
		{
			try
			{
				return await _movieServiceClient.GetMovieById(id);
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieManagerException(
					string.Format("An unexpected error happend trying to get movie by id: {0}", id), e);
			}
		}

		public async Task<MoviePage> GetUpcomingMovies(int page)
		{
			try
			{
				if (page < 0)
					throw new ArctouchMovieManagerException(string.Format("Invalid page value {0}", page));

				return await _movieServiceClient.GetUpcomingMovies(page);
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieManagerException(
					string.Format("An unexpected error happend trying to get upcoming movies for page {0}.", page), e);
			}
		}

		public async Task<MoviePage> Search(string query, int page)
		{
			try
			{
				if (page < 0)
					throw new ArctouchMovieManagerException(string.Format("Invalid page value {0}", page));
				
				if (string.IsNullOrWhiteSpace(query))
					throw new ArctouchMovieManagerException(string.Format("Invalid query value. It must not be null or empty space", page));

				return await _movieServiceClient.Search(query, page);
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieManagerException(
					string.Format("An unexpected error happend trying to search movies with query {0} for page", query, page), e);
			}
		}
	}
}
