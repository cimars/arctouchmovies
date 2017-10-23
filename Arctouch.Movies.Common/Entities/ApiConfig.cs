using Arctouch.Movies.Common.Exceptions;
using System.Collections.Generic;

namespace Arctouch.Movies.Common.Entities
{
	/// <summary>
	/// Holds data related to the Api Configuration on TheMovieDB. It is retrieved from API.
	/// </summary>
	public class ApiConfig
	{
		#region Fields
		private string _baseUrlForSmallPoster, _baseUrlForMediumPoster;
		#endregion

		#region Properties
		public string BaseUrl { get; set; }

		public string SecureBaseUrl { get; set; }

		public IList<string> PosterSizes { get; set; }

		public string BaseUrlForSmallPoster
		{
			get
			{
				if (!string.IsNullOrEmpty(_baseUrlForSmallPoster))
					return _baseUrlForSmallPoster;

				if (string.IsNullOrEmpty(BaseUrl))
					throw new ArctouchMovieException("Invalid base url received.");

				if (PosterSizes == null || PosterSizes.Count == 0)
					throw new ArctouchMovieException("Invalid poster sizes received.");

				// We are taking the first poster size for small size
				var firstPorsterSize = PosterSizes[0];
				_baseUrlForSmallPoster = string.Format("{0}{1}", BaseUrl, firstPorsterSize);

				return _baseUrlForSmallPoster;
			}
		}

		public string BaseUrlForMediumPoster
		{
			get
			{
				if (!string.IsNullOrEmpty(_baseUrlForMediumPoster))
					return _baseUrlForMediumPoster;

				if (string.IsNullOrEmpty(BaseUrl))
					throw new ArctouchMovieException("Invalid base url received.");

				if (PosterSizes == null || PosterSizes.Count < 2)
					throw new ArctouchMovieException("Invalid poster sizes received.");

				// We are taking the second item of the poster sizes for medium size
				var firstPorsterSize = PosterSizes[1];
				_baseUrlForMediumPoster = string.Format("{0}{1}", BaseUrl, firstPorsterSize);

				return _baseUrlForMediumPoster;
			}
		}

		public static ApiConfig DefaultConfig
		{
			get
			{
				return new ApiConfig
				{
					BaseUrl = "http://image.tmdb.org/t/p/",
					SecureBaseUrl = "https://image.tmdb.org/t/p/",
					PosterSizes = new string[]
					{
						"w92",
						"w154",
						"w185",
						"w342",
						"w500",
						"w780",
						"original"
					}
				};
			}
		}
		#endregion
	}
}
