using Arctouch.Movies.Common.Interfaces.Services;
using NUnit.Framework;
using Microsoft.Practices.Unity;

namespace Arctouch.Movies.Services.Tests
{
	[TestFixture()]
	public class GenreServiceTests : BaseServiceTests
	{
		[Test]
		public async void TestGetGenres()
		{
			var genreService = DIContainer.Resolve<IGenreService>();
			var genres = await genreService.GetAll();
			Assert.IsNotNull(genres);
			if (genres.Count > 0)
			{
				foreach (var genre in genres)
				{
					Assert.IsNotNull(genre);
					Assert.Greater(genre.Id, 0);
					Assert.IsNotNullOrEmpty(genre.Name);
				}
			}
		}

		// We can add more tests for bad cases, and so on.
	}
}