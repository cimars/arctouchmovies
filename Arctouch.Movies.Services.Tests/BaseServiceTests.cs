using Arctouch.Movies.Common.Context;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Config;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Arctouch.Movies.Services.Tests
{
	public class BaseServiceTests
	{
		protected IUnityContainer DIContainer { get; set; }

		[SetUp]
		public virtual void BeforeEachTest()
		{
			DIContainer = new UnityContainer();
			DIConfigurator.Configure(DIContainer);
			MovieAppContext.DIContainer = DIContainer;
			MovieAppContext.CurrentApiConfig = ApiConfig.DefaultConfig;
		}
	}
}