using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Arctouch.Movies.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Splash : ContentPage
	{
		public Splash()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}