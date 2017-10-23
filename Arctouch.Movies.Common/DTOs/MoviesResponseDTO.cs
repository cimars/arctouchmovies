using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Arctouch.Movies.Common.DTOs
{
	[DataContract]
	public class MoviesResponseDTO
	{
		[DataMember(Name = "results")]
		public IList<MovieDTO> Movies { get; set; }

		[DataMember(Name = "page")]
		public int Page { get; set; }

		[DataMember(Name = "total_results")]
		public int TotalResults { get; set; }

		[DataMember(Name = "total_pages")]
		public int TotalPages { get; set; }
	}
}