using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Interfaces.Managers;
using Arctouch.Movies.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arctouch.Movies.Managers
{
	public class GenreManager : IGenreManager
	{
		private readonly IGenreService _genreServiceClient;

		public GenreManager(IGenreService genreServiceClient)
		{
			_genreServiceClient = genreServiceClient;
		}

		public async Task<IList<Genre>> GetAllGenres()
		{
			try
			{
				return await _genreServiceClient.GetAll();
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieManagerException("An unexpected error happend trying to get all genres", e);
			}
		}
	}
}