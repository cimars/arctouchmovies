using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Arctouch.Movies.UI.ViewModels
{
	public class BaseViewModel : BindableBase, INavigationAware
	{
		#region Fields
		protected readonly INavigationService _navigationService;
		protected readonly IPageDialogService _dialogService;
		#endregion

		#region Constructors
		public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
		}
		#endregion

		#region INavigationAware
		public async virtual void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public async virtual void OnNavigatingTo(NavigationParameters parameters)
		{

		}

		public async virtual void OnNavigatedTo(NavigationParameters parameters)
		{

		}
		#endregion
	}
}