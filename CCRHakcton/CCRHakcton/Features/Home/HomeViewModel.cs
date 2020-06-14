using System.Windows.Input;
using Xamarin.Forms;

namespace Core
{
    public class HomeViewModel : BaseViewModel
    {

        #region Commands
        public ICommand LoginCommand { get; }
        public ICommand CadastroCommand { get; }
        public ICommand ChangeUserCommand { get; set; }
        #endregion

        #region Init
        public HomeViewModel(INavigationService navigationService) : base(navigationService)
        {

            //LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
            //CadastroCommand = new Command(async () => await ExecuteCadastroCommandAsync());
            //ChangeUserCommand = new Command((user) => ExecuteChangeUserCommand(user));
        }

        #endregion

    }
}
