using Arctouch.Movies.Common.Entities;
using Arctouch.Movies.Common.Interfaces.Managers;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Arctouch.Movies.UI.ViewModels
{
	public class MoviesViewModel : BaseViewModel, INavigationAware
	{
		#region Fields
		private readonly IMovieManager _movieManager;
		private IList<Movie> _movies;
		private bool _alreadyLoaded;
		#endregion

		#region IsLoading
		// To avoid double or more loading of pages
		private readonly object locker = new object();
		private volatile bool _isLoading;

		private bool TryEnableLoading()
		{
			lock (locker)
			{
				if (_isLoading)
					return false;

				_isLoading = true;
				return true;
			}
		}

		private void DisableLoading()
		{
			lock (locker)
			{
				_isLoading = false;
			}
		}
		#endregion

		#region Pages
		public int LastPageLoaded { get; set; }

		public int TotalPages { get; set; }
		#endregion

		#region IsProcessing
		private bool _isProcessing = true;
		public bool IsProcessing
		{
			get { return _isProcessing; }
			set { SetProperty(ref _isProcessing, value); }
		}
		#endregion

		#region SearchQuery
		private string _searchQuery = string.Empty;
		public string SearchQuery
		{
			get { return _searchQuery; }
			set { SetProperty(ref _searchQuery, value); }
		}
		#endregion

		#region ShowMovies
		private bool _showMovies = false;
		public bool ShowMovies
		{
			get { return _showMovies; }
			set { SetProperty(ref _showMovies, value); }
		}
		#endregion

		#region Movies
		private ObservableCollection<Movie> _moviesCollection;
		public ObservableCollection<Movie> Movies
		{
			get { return _moviesCollection; }
			set { SetProperty(ref _moviesCollection, value); }
		}
		#endregion

		#region Movie List Selected Command
		private DelegateCommand<Movie> _movieSelectedCommand;
		public DelegateCommand<Movie> MovieSelectedCommand => _movieSelectedCommand == null
			? (_movieSelectedCommand = new DelegateCommand<Movie>(MovieSelected)) : _movieSelectedCommand;

		private async void MovieSelected(Movie movie)
		{
			try
			{
				var p = new NavigationParameters();
				p.Add("movie", movie);

				await _navigationService.NavigateAsync("MovieDetails", p);
			}
			catch (Exception e)
			{
				await _dialogService.DisplayAlertAsync("Error", "An error happened loading movie details", "Ok");
			}
		}
		#endregion

		#region Constructors
		public MoviesViewModel(INavigationService navigationService, IPageDialogService dialogService,
			IMovieManager movieManager)
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
				if (_alreadyLoaded)
					return;

				var moviePage = await _movieManager.GetUpcomingMovies(1);
				_movies = moviePage.Movies;
				LastPageLoaded = 1;
				TotalPages = moviePage.TotalPages;
				IsProcessing = false;
				Movies = new ObservableCollection<Movie>(_movies);
				ShowMovies = true;
				_alreadyLoaded = true;
			}
			catch (Exception e)
			{
				await _dialogService.DisplayAlertAsync("Error", e.Message, "Ok");
			}
		}
		#endregion

		#region Search
		private DelegateCommand _searchCommand;
		public DelegateCommand SearchCommand => _searchCommand == null
			? (_searchCommand = new DelegateCommand(Search)) : _searchCommand;

		private async void Search()
		{
			try
			{
				var searchQuery = SearchQuery;
				if (!_alreadyLoaded && string.IsNullOrEmpty(searchQuery))
					return;
				
				MoviePage moviePage;
				if (string.IsNullOrEmpty(searchQuery))
				{
					moviePage = await _movieManager.GetUpcomingMovies(1);
				}
				else
				{
					moviePage = await _movieManager.Search(searchQuery, 1);
					if (searchQuery != SearchQuery)
						return;
				}

				_movies = moviePage.Movies;
				LastPageLoaded = 1;
				TotalPages = moviePage.TotalPages;
				Movies = new ObservableCollection<Movie>(_movies);
			}
			catch (Exception)
			{
				IsProcessing = false;
				// Log exception
				await _dialogService.DisplayAlertAsync("Error", "Error happened searching movies", "Ok");
			}
		}
		#endregion

		#region Load Next Page Command
		private DelegateCommand _loadMoreMoviesCommand;
		public DelegateCommand LoadMoreMoviesCommand => _loadMoreMoviesCommand == null
			? (_loadMoreMoviesCommand = new DelegateCommand(LoadMoreMovies)) : _loadMoreMoviesCommand;

		private async void LoadMoreMovies()
		{
			try
			{
				if (LastPageLoaded < TotalPages)
				{
					if (!TryEnableLoading())
						return;

					var pageToLoad = LastPageLoaded + 1;
					var searchQuery = SearchQuery;
					MoviePage moviePage;
					if (string.IsNullOrEmpty(searchQuery))
					{
						moviePage = await _movieManager.GetUpcomingMovies(pageToLoad);
					}
					else
					{
						moviePage = await _movieManager.Search(searchQuery, pageToLoad);
					}
					
					foreach (var movie in moviePage.Movies)
					{
						Movies.Add(movie);
					}
					LastPageLoaded = pageToLoad;
					DisableLoading();
				}
			}
			catch (Exception)
			{
				DisableLoading();
				// Log exception
				await _dialogService.DisplayAlertAsync("Error", "Error happened trying to load more movies", "Ok");
			}
		}
		#endregion
	}
}