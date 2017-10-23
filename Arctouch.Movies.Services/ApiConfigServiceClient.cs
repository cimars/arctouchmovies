using Arctouch.Movies.Common.DTOs;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Exceptions;
using Arctouch.Movies.Common.Helpers;
using Arctouch.Movies.Common.Interfaces.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arctouch.Movies.Services
{
	public class ApiConfigServiceClient : BaseApiServiceClient, IApiConfigService
	{
		public ApiConfigServiceClient(string baseUrl, string apiKey, string language)
			: base(baseUrl, apiKey, language)
		{ }

		public async Task<ApiConfig> GetApiConfig()
		{
			try
			{
				var httpRequest = CreateHttpRequest();
				httpRequest.Method = HttpMethod.Get;
				var requestUri = UrlHelper.BuildUri(BaseUrl, "3/configuration",
					GetBaseUrlParams());
				httpRequest.RequestUri = requestUri;

				var httpClient = CreateHttpClient();
				var response = await httpClient.SendAsync(httpRequest);
				if (response.StatusCode != HttpStatusCode.OK)
				{
					throw new ArctouchMovieServiceException(
						string.Format("Error getting api config from API. An unexpected status code: {0}",
						response.StatusCode));
				}

				var responseBody = await response.Content.ReadAsStringAsync();
				var responseDto = JsonConvert.DeserializeObject<ApiConfigResponseDTO>(responseBody);
				return new ApiConfig
				{
					BaseUrl = responseDto.ApiConfigData.BaseUrl,
					SecureBaseUrl = responseDto.ApiConfigData.BaseUrl,
					PosterSizes = responseDto.ApiConfigData.PosterSizes
				};
			}
			catch (ArctouchMovieException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new ArctouchMovieServiceException(
					"An unexpected error happend trying to get api config from web api", e);
			}
		}
	}
}