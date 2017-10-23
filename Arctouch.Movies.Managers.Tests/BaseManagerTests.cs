using Arctouch.Movies.Common.Context;
using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Config;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Arctouch.Movies.Managers.Tests
{
	/// <summary>
	/// Base class for all manager tests to share common logic and data
	/// </summary>
	public class BaseManagerTests
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