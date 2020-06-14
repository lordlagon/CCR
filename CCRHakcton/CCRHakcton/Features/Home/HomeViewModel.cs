using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Core
{
    public class HomeViewModel : BaseViewModel
    {

        #region Commands
      
        public ICommand MyAccountCommand { get; }
        public ICommand ParadasCommand { get; }
        public ICommand CuponsCommand { get; set; }
        #endregion

        #region Init
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {

            MyAccountCommand = new Command(async () => await ExecuteMyAccountCommandAsync());
            ParadasCommand = new Command(async () => await ExecuteParadasCommandAsync());
            CuponsCommand = new Command(() => ExecuteCuponsCommand());
        }

        private void ExecuteCuponsCommand()
        {
            throw new NotImplementedException();
        }

        private Task ExecuteParadasCommandAsync()
        {
            throw new NotImplementedException();
        }

        async Task ExecuteMyAccountCommandAsync()
            => await _navigationService.NavigateToAsync<MyAccountViewModel>();


        #endregion

    }
}
