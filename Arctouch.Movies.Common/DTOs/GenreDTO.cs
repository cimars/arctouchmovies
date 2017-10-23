using System.Runtime.Serialization;

namespace Arctouch.Movies.Common.DTOs
{
	[DataContract]
	public class GenreDTO
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }
	}
}