using Arctouch.Movies.Common.Context;
using Arctouch.Movies.Common.DTOs;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Helpers;
using Arctouch.Movies.Common.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arctouch.Movies.Services
{
	public class MoviesServiceClient : BaseApiServiceClient, IMovieService
	{
		public MoviesServiceClient(string baseUrl, string apiKey, string language)
			: base(baseUrl, apiKey, language)
		{ }

		public async Task<Movie> GetMovieById(int movieId)
		{
			try
			{
				var httpRequest = CreateHttpRequest();
				httpRequest.Method = HttpMethod.Get;
				var urlParams = GetBaseUrlParams();
				httpRequest.RequestUri = UrlHelper.BuildUri(BaseUrl, string.Format("3/movie/{0}", movieId),
					urlParams);

				var httpClient = CreateHttpClient();
				var response = await httpClient.SendAsync(httpRequest);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ArctouchMovieServiceException(
						string.Format("Error getting movies from API. An unexpected status code: {0}",
						response.StatusCode));
				}

				var responseBody = await response.Content.ReadAsStringAsync();
				var responseDto = JsonConvert.DeserializeObject<MovieDTO>(responseBody);

				var movie = new Movie
				{
					Id = responseDto.Id,
					Title = responseDto.Title,
					PosterPath = responseDto.PosterPath,
					Overview = responseDto.Overview,
				};

				if (!string.IsNullOrEmpty(responseDto.ReleaseDate))
					movie.ReleaseDate = DateTime.Parse(responseDto.ReleaseDate, CultureInfo.InvariantCulture);

				foreach (var genreDto in responseDto.Genres)
				{
					movie.Genres.Add(new Genre()
					{
						Id = genreDto.Id,
						Name = genreDto.Name
					});
				}

				return movie;
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieServiceException(
					string.Format("An unexpected error happend trying to get movie {0} from web api", movieId), e);
			}
		}

		public async Task<MoviePage> GetUpcomingMovies(int page)
		{
			return await GetMovies("3/movie/upcoming", page, string.Empty);
		}

		public async Task<MoviePage> Search(string query, int page)
		{
			return await GetMovies("3/search/movie", page, query);
		}

		private async Task<MoviePage> GetMovies(string path, int page, string query)
		{
			try
			{
				var httpRequest = CreateHttpRequest();
				httpRequest.Method = HttpMethod.Get;
				var urlParams = GetBaseUrlParams();
				urlParams.Add("page", page.ToString());
				if (!string.IsNullOrEmpty(query))
				{
					urlParams.Add("query", query);
					// By default we are ignoring adult genre for the results
					urlParams.Add("include_adult", "false");
				}
				httpRequest.RequestUri = UrlHelper.BuildUri(BaseUrl, path,
					urlParams);

				var httpClient = CreateHttpClient();
				var response = await httpClient.SendAsync(httpRequest);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ArctouchMovieServiceException(
						string.Format("Error getting movies from API. An unexpected status code: {0}, path: {1}, page: {2}, query: {3}",
						response.StatusCode, path, page, query));
				}

				var responseBody = await response.Content.ReadAsStringAsync();
				var responseDto = JsonConvert.DeserializeObject<MoviesResponseDTO>(responseBody);
				var movies = new List<Movie>();

				var allGenres = await MovieAppContext.GenresCache.GetGenres();

				foreach (var movieDto in responseDto.Movies)
				{
					var movie = new Movie
					{
						Id = movieDto.Id,
						Title = movieDto.Title,
						PosterPath = movieDto.PosterPath,
						Overview = movieDto.Overview,
						GenreIds = movieDto.GenreIds
					};

					if (!string.IsNullOrEmpty(movieDto.ReleaseDate))
					{
						movie.ReleaseDate = DateTime.Parse(movieDto.ReleaseDate, CultureInfo.InvariantCulture);
					}

					foreach (var genreId in movie.GenreIds)
					{
						var genre = allGenres.FirstOrDefault(g => g.Id == genreId);
						if (genre == null)
						{
							continue;
							// In some cases, the movie api is turning no existent genderId 
							// So, we could throw an exception if gender id is not found
						}

						movie.Genres.Add(genre);
					}

					movies.Add(movie);
				}

				return new MoviePage
				{
					Page = responseDto.Page,
					TotalPages = responseDto.TotalPages,
					TotalResults = responseDto.TotalResults,
					Movies = movies
				};
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieServiceException(
					string.Format("Error getting movies from API. An unexpected error. Path: {0}, page: {1}, query: {2}",
						path, page, query), e);
			}
		}
	}
}