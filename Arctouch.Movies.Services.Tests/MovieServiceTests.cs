using Arctouch.Movies.Common.Interfaces.Services;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Arctouch.Movies.Services.Tests
{
	[TestFixture()]
	public class MovieServiceTests : BaseServiceTests
	{
		[Test]
		public async void TestGetUpcomingMovies()
		{
			var movieService = DIContainer.Resolve<IMovieService>();
			var moviePage = await movieService.GetUpcomingMovies(1);
			Assert.IsNotNull(moviePage);
			Assert.AreEqual(1, moviePage.Page);
			Assert.Greater(moviePage.TotalPages, 0);
			Assert.Greater(moviePage.TotalResults, 0);
			if (moviePage.Movies.Count > 0)
			{
				foreach (var movie in moviePage.Movies)
				{
					Assert.IsNotNull(movie);
					Assert.Greater(movie.Id, 0);
					Assert.IsNotNullOrEmpty(movie.Title);
					Assert.IsNotNullOrEmpty(movie.PosterPath);
					Assert.IsNotNullOrEmpty(movie.Overview);
					Assert.IsNotNull(movie.GenreIds);
					Assert.Greater(movie.GenreIds.Count, 0);
				}
			}
		}

		[Test]
		public async void TestSearchMovies()
		{
			var movieService = DIContainer.Resolve<IMovieService>();
			var moviePage = await movieService.Search("matrix", 1);
			Assert.IsNotNull(moviePage);
			Assert.AreEqual(1, moviePage.Page);
			Assert.Greater(moviePage.TotalPages, 0);
			Assert.Greater(moviePage.TotalResults, 0);
			if (moviePage.Movies.Count > 0)
			{
				foreach (var movie in moviePage.Movies)
				{
					Assert.IsNotNull(movie);
					Assert.Greater(movie.Id, 0);
					Assert.IsNotNullOrEmpty(movie.Title);
					// Movie api is not returning a valid poster path for all movies
					//Assert.IsNotNullOrEmpty(movie.PosterPath);
					// Movie api is not returning valid overview for all movies
					//Assert.IsNotNullOrEmpty(movie.Overview);
					Assert.IsNotNull(movie.GenreIds);
					// Movie api is not returning valid genre ids for all movies
					//Assert.Greater(movie.GenreIds.Count, 0);
				}
			}
		}

		[Test]
		public async void TestMovieDetails()
		{
			var movieService = DIContainer.Resolve<IMovieService>();
			// Getting matrix movie
			var movie = await movieService.GetMovieById(603);
			Assert.IsNotNull(movie);
			Assert.Greater(movie.Id, 0);
			Assert.IsNotNullOrEmpty(movie.Title);
			Assert.IsTrue(movie.ReleaseDate.HasValue);
			Assert.IsNotNullOrEmpty(movie.PosterPath);
			Assert.IsNotNullOrEmpty(movie.FullPosterUrlForDetails);
			Assert.IsNotNullOrEmpty(movie.Overview);
			Assert.IsNotNull(movie.Genres);
			Assert.Greater(movie.Genres.Count, 0);
		}

		// We can add more tests for bad cases, and so on.
	}
}