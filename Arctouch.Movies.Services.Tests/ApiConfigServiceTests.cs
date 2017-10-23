using Arctouch.Movies.Common.Interfaces.Services;
using NUnit.Framework;
using Microsoft.Practices.Unity;

namespace Arctouch.Movies.Services.Tests
{
	[TestFixture()]
	public class ApiConfigServiceTests : BaseServiceTests
	{
		[Test]
		public async void TestGetApiConfig()
		{
			var apiConfigService = DIContainer.Resolve<IApiConfigService>();
			var apiConfig = await apiConfigService.GetApiConfig();
			Assert.IsNotNull(apiConfig);
			Assert.IsNotNullOrEmpty(apiConfig.BaseUrl);
			Assert.IsNotNullOrEmpty(apiConfig.SecureBaseUrl);
			Assert.IsNotNull(apiConfig.PosterSizes);
			if (apiConfig.PosterSizes.Count > 0)
			{
				foreach (var posterSize in apiConfig.PosterSizes)
				{
					Assert.IsNotNullOrEmpty(posterSize);
				}
			}
		}

		// We can add more tests for bad cases, and so on.
	}
}