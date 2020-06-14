using System.Windows.Input;
using Xamarin.Forms;

namespace Core
{
    public class MainViewModel : BaseViewModel
    {

        #region Commands
        public ICommand LoginCommand { get; }
        public ICommand CadastroCommand { get; }
        public ICommand ChangeUserCommand { get; set; }
        #endregion

        #region Init
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {

            //LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
            //CadastroCommand = new Command(async () => await ExecuteCadastroCommandAsync());
            //ChangeUserCommand = new Command((user) => ExecuteChangeUserCommand(user));
        }

        #endregion

    }
}
