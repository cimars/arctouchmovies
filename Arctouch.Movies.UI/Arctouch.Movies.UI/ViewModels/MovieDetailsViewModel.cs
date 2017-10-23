using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Interfaces.Managers;
using Prism.Navigation;
using Prism.Services;
using System;

namespace Arctouch.Movies.UI.ViewModels
{
	public class MovieDetailsViewModel : BaseViewModel, INavigationAware
	{
		#region Fields
		private readonly IMovieManager _movieManager;
		#endregion

		#region Movie
		private Movie _movie;
		public Movie Movie
		{
			get { return _movie; }
			set { SetProperty(ref _movie, value); }
		}
		#endregion

		#region IsProcessing
		private bool _isProcessing = true;
		public bool IsProcessing
		{
			get { return _isProcessing; }
			set { SetProperty(ref _isProcessing, value); }
		}
		#endregion

		#region MovieTitle
		private string _movieTitle = string.Empty;
		public string MovieTitle
		{
			get { return _movieTitle; }
			set { SetProperty(ref _movieTitle, value); }
		}
		#endregion

		#region ShowMovie
		private bool _showMovie = false;
		public bool ShowMovie
		{
			get { return _showMovie; }
			set { SetProperty(ref _showMovie, value); }
		}
		#endregion

		#region Constructors
		public MovieDetailsViewModel(INavigationService navigationService,
			IPageDialogService dialogService, IMovieManager movieManager)
			: base(navigationService, dialogService)
		{
			_movieManager = movieManager;
		}
		#endregion

		#region INavigationAware
		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			try
			{
				IsProcessing = true;
				var movieSelected = (Movie)parameters["movie"];
				Movie = movieSelected;
				MovieTitle = movieSelected.Title;
				var movie = await _movieManager.GetMovieById(movieSelected.Id);
				Movie = movie;
				ShowMovie = true;
			}
			catch (Exception)
			{
				// Log exception
				await _dialogService.DisplayAlertAsync("Error", "An unexpected error happened getting movie details", "Ok");
			}
			finally
			{
				IsProcessing = false;
			}
		}
		#endregion
	}
}