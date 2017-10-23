using System.Collections.Generic;

namespace Arctouch.Movies.Common.Entities
{
	public class MoviePage
	{
		public IList<Movie> Movies { get; set; }

		public int Page { get; set; }

		public int TotalResults { get; set; }

		public int TotalPages { get; set; }
	}
}