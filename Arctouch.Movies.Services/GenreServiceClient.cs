using Arctouch.Movies.Common.DTOs;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Helpers;
using Arctouch.Movies.Common.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arctouch.Movies.Services
{
	public class GenreServiceClient : BaseApiServiceClient, IGenreService
	{
		public GenreServiceClient(string baseUrl, string apiKey, string language)
			: base(baseUrl, apiKey, language)
		{ }

		public async Task<IList<Genre>> GetAll()
		{
			try
			{
				var httpRequest = CreateHttpRequest();
				httpRequest.Method = HttpMethod.Get;
				httpRequest.RequestUri = UrlHelper.BuildUri(BaseUrl, "3/genre/movie/list",
					GetBaseUrlParams());

				var httpClient = CreateHttpClient();
				var response = await httpClient.SendAsync(httpRequest);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ArctouchMovieServiceException(
						string.Format("Error getting genres from API. An unexpected status code: {0}",
						response.StatusCode));
				}

				var responseBody = await response.Content.ReadAsStringAsync();
				var genreResponseDto = JsonConvert.DeserializeObject<GenresResponseDTO>(responseBody);
				var genres = new List<Genre>();

				foreach (var genreDto in genreResponseDto.Genres)
				{
					genres.Add(new Genre
					{
						Id = genreDto.Id,
						Name = genreDto.Name
					});
				}

				return genres;
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieServiceException("An unexpected error happend trying to get all genres from web api.", e);
			}
		}
	}
}