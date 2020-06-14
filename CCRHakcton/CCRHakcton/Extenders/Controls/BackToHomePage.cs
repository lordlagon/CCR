using System;
using Xamarin.Forms;

namespace Core
{
    public abstract class BackToHomePage : ContentPage
    {
        protected INavigationService _navigationService => App.Instance.Container.Resolve<INavigationService>();

        public BackToHomePage()
        {
        }

        protected override bool OnBackButtonPressed()
        {
            _navigationService.NavigateAndClearBackStackAsync<HomeViewModel>();
            return true;
        }
    }
}
