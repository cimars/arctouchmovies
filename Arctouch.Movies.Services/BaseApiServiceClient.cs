using System.Collections.Generic;
using System.Net.Http;

namespace Arctouch.Movies.Services
{
	/// <summary>
	/// Base class for api service clients to allow inheritance common data and logic
	/// </summary>
	public class BaseApiServiceClient
	{
		protected string BaseUrl { get; set; }

		protected string ApiKey { get; set; }

		protected string Language { get; set; }

		protected BaseApiServiceClient(string baseUrl, string apiKey, string language)
		{
			BaseUrl = baseUrl;
			ApiKey = apiKey;
			Language = language;
		}

		protected static HttpClient CreateHttpClient()
		{
			var httpClient = new HttpClient();
			// Here can come additional common configuration for httpClient
			return httpClient;
		}

		protected static HttpRequestMessage CreateHttpRequest()
		{
			var httpRequest = new HttpRequestMessage();
			// Here can come additional common configuration for httpRequest
			return httpRequest;
		}

		/// <summary>
		/// Return common url query params for all requests
		/// </summary>
		/// <returns>Dictionary holding common url query params for all requests</returns>
		protected IDictionary<string, string> GetBaseUrlParams()
		{
			var baseUrlParams = new Dictionary<string, string>();
			baseUrlParams.Add("api_key", ApiKey);
			baseUrlParams.Add("language", Language);

			return baseUrlParams;
		}
	}
}