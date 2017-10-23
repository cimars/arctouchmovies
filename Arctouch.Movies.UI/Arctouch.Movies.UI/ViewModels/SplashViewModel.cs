using Arctouch.Movies.Common.Context;
using Arctouch.Movies.Common.Interfaces.Managers;
using Prism.Navigation;
using Prism.Services;
using System;

namespace Arctouch.Movies.UI.ViewModels
{
	public class SplashViewModel : BaseViewModel, INavigationAware
	{
		#region Fields
		private readonly IConfigManager _configManager;
		#endregion

		#region Constructors
		public SplashViewModel(INavigationService navigationService, 
			IPageDialogService dialogService, IConfigManager configManager)
			: base(navigationService, dialogService)
		{
			_configManager = configManager;
		}
		#endregion

		#region INavigationAware
		public async override void OnNavigatedTo(NavigationParameters parameters)
		{
			try
			{
				// Getting MovieDB Api Configuration to get base url for images
				var apiConfig = await _configManager.GetApiConfig();

				MovieAppContext.CurrentApiConfig = apiConfig;

				await _navigationService.NavigateAsync("/NavigationPage/Movies");
			}
			catch (Exception)
			{
				// Log exception, we could have an error screen to redirect,
				// but for now we are only showing an alert error
				await _dialogService.DisplayAlertAsync("Error", "An unexpected error happened starting app", "Ok");
			}
		}
		#endregion
	}
}
