using System.Runtime.Serialization;

namespace Arctouch.Movies.Common.DTOs
{
	[DataContract]
	public class ApiConfigResponseDTO
	{
		[DataMember(Name = "images")]
		public ApiConfigDTO ApiConfigData { get; set; }
	}
}