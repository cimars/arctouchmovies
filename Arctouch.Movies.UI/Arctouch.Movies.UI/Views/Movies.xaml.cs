using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.UI.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Arctouch.Movies.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Movies : ContentPage
	{
		public Movies()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			// Scrolling logic
			MoviesListView.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
			{
				try
				{
					var item = e.Item as Movie;
					var uiData = ((MoviesViewModel)this.BindingContext).Movies;
					int index = uiData.IndexOf(item);
					if (uiData.Count - 2 <= index)
					{
						MoviesListView.BeginRefresh();
						((MoviesViewModel)this.BindingContext).LoadMoreMoviesCommand.Execute();
						MoviesListView.EndRefresh();
					}
				}
				catch (Exception)
				{
					// Log exception using for e.g. Xamarin Insights
					DisplayAlert("Error", "An error happened trying to load more movies", "Ok");
				}
			};
		}

		private void MoviesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			try
			{
				var movie = e.SelectedItem as Movie;
				if (movie == null)
					return;

				((MoviesViewModel)this.BindingContext).MovieSelectedCommand.Execute(movie);
				((ListView)sender).SelectedItem = null;
			}
			catch (Exception)
			{
				// Log exception using for e.g. Xamarin Insights
				DisplayAlert("Error", "An error happened trying to load movie details", "Ok");
			}
		}

		private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				((MoviesViewModel)this.BindingContext).SearchCommand.Execute();
			}
			catch (Exception)
			{
				// Log exception using for e.g. Xamarin Insights
				DisplayAlert("Error", "An error happened searching movies", "Ok");
			}
		}
	}
}