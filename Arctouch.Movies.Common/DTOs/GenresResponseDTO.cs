using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Arctouch.Movies.Common.DTOs
{
	[DataContract]
	public class GenresResponseDTO
	{
		[DataMember(Name = "genres")]
		public IList<GenreDTO> Genres { get; set; }
	}
}