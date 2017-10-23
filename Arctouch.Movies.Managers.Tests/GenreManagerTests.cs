using Arctouch.Movies.Common.Interfaces.Managers;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Arctouch.Movies.Managers.Tests
{
	[TestFixture()]
	public class GenreManagerTests : BaseManagerTests
	{
		[Test]
		public async void TestGetGenres()
		{
			var genreManager = DIContainer.Resolve<IGenreManager>();
			var genres = await genreManager.GetAllGenres();
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
