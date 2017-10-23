using Arctouch.Movies.Common.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arctouch.Movies.Common.Entities
{
	public class Movie
    {
		public Movie()
		{
			GenreIds = new List<int>();
			Genres = new List<Genre>();
		}

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string PosterPath { get; set; }

        public string Overview { get; set; }

		public IList<int> GenreIds { get; set; }

        public IList<Genre> Genres { get; set; }

		public string GenresFormatted
		{
			get
			{
				if (Genres.Count == 0)
					return "No Genres";

				var sb = new StringBuilder();
				foreach (var genre in Genres)
				{
					if (sb.Length > 0)
					{
						sb.Append(", ");
					}

					sb.Append(genre.Name);
				}

				return sb.ToString();
			}
		}

		public string FullPosterUrl
		{
			get
			{
				if (string.IsNullOrEmpty(PosterPath))
					return string.Empty;

				return string.Format("{0}{1}", MovieAppContext.CurrentApiConfig.BaseUrlForSmallPoster, PosterPath);
			}
		}

		public string FullPosterUrlForDetails
		{
			get
			{
				if (string.IsNullOrEmpty(PosterPath))
					return string.Empty;

				return string.Format("{0}{1}", MovieAppContext.CurrentApiConfig.BaseUrlForMediumPoster, PosterPath);
			}
		}

		public string ReleaseDateFormatted
		{
			get
			{
				if (ReleaseDate.HasValue)
					return ReleaseDate.Value.ToString("MM/dd/yyyy");

				return "No Valid Release Date";
			}
		}
	}
}
