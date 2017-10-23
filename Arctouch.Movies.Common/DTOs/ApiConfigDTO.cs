using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Arctouch.Movies.Common.DTOs
{
	[DataContract]
	public class ApiConfigDTO
	{
		[DataMember(Name = "base_url")]
		public string BaseUrl { get; set; }

		[DataMember(Name = "secure_base_url")]
		public string SecureBaseUrl { get; set; }

		[DataMember(Name = "poster_sizes")]
		public IList<string> PosterSizes { get; set; }
	}
}