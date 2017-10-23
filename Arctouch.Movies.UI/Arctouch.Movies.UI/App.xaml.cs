using Arctouch.Movies.Common.Context;
using Arctouch.Movies.Config;
using Arctouch.Movies.UI.Views;
using Prism.Unity;
using Xamarin.Forms;

namespace Arctouch.Movies.UI
{
	public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/Splash");
		}

		protected override void RegisterTypes()
		{
			MovieAppContext.DIContainer = Container;
			DIConfigurator.Configure(Container);

			Container.RegisterTypeForNavigation<NavigationPage>();
			Container.RegisterTypeForNavigation<Splash>();
			Container.RegisterTypeForNavigation<Views.Movies>();
			Container.RegisterTypeForNavigation<MovieDetails>();
		}
	}
}