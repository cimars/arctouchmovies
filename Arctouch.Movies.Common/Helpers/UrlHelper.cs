using System;
using System.Collections.Generic;
using System.Net;

namespace Arctouch.Movies.Common.Helpers
{
	/// <summary>
	/// Helper class to build urls with query params
	/// </summary>
	public static class UrlHelper
	{
		public static string BuildUrl(string baseUrl, string resource)
		{
			if (baseUrl.EndsWith("/"))
			{
				if (resource.StartsWith("/"))
				{
					return string.Format("{0}{1}", baseUrl, resource.Substring(1));
				}
				return string.Format("{0}{1}", baseUrl, resource);
			}
			else
			{
				if (resource.StartsWith("/"))
				{
					return string.Format("{0}{1}", baseUrl, resource);
				}
				return string.Format("{0}/{1}", baseUrl, resource);
			}
		}

		/// <summary>
		/// Builds an uri using a base url, a resource path, and query params
		/// </summary>
		/// <param name="baseUrl">Base url</param>
		/// <param name="resource">Resource path</param>
		/// <param name="urlParams">Url query params</param>
		/// <returns></returns>
		public static Uri BuildUri(string baseUrl, string resource, IDictionary<string, string> urlParams)
		{
			var uri = new UriBuilder(BuildUrl(baseUrl, resource));
			if (urlParams.Count > 0)
			{
				uri.Query = ToQueryString(urlParams);
			}

			return uri.Uri;
		}

		public static string ToQueryString(IDictionary<string, string> urlParams)
		{
			var urlQueryParams = new List<string>();
			foreach (var urlParam in urlParams)
			{
				urlQueryParams.Add(string.Format("{0}={1}",
						 WebUtility.UrlEncode(urlParam.Key),
						 WebUtility.UrlEncode(urlParam.Value)));
			}

			return string.Join("&", urlQueryParams);
		}
	}
}
