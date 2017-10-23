using Arctouch.Movies.Common.Interfaces.Managers;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Arctouch.Movies.Managers.Tests
{
	[TestFixture()]
	public class ConfigManagerTests : BaseManagerTests
	{
		[Test]
		public async void TestGetConfig()
		{
			var configManager = DIContainer.Resolve<IConfigManager>();
			var apiConfig = await configManager.GetApiConfig();
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