using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Core
{
    public class LoginViewModel : BaseViewModel
    {
        #region Properties
        string _user;
        public string User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        string _error = string.Empty;
        public string Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }
        #endregion

        #region Services
        readonly IDialogService _dialogService;
        #endregion

        #region Commands
        public ICommand LoginCommand { get; }
        public ICommand CadastroCommand { get; }
        public ICommand ChangeUserCommand { get; set; }
        #endregion

        #region Init
        public LoginViewModel(INavigationService navigationService,
                              IDialogService dialogService) : base(navigationService)
        {
            _dialogService = dialogService;

            LoginCommand = new Command(async () => await ExecuteLoginCommandAsync());
            CadastroCommand = new Command(async () => await ExecuteCadastroCommandAsync());
            ChangeUserCommand = new Command((user) => ExecuteChangeUserCommand(user));
        }

        #endregion

        #region ExecuteCommand

        void ExecuteChangeUserCommand(object user)
        {
            if (user is string usr)
            {
                switch (usr)
                {
                    case "Peter":
                        User = "peter.novassat@sikur.dev";
                        break;
                    case "Andre":
                        User = "andre.macedo@sikur.dev";
                        break;
                    case "Tiago":
                        User = "sikurdev+1@sikur.dev";
                        break;
                    case "Bruno":
                        User = "bruno.silva@sikur.dev";
                        break;
                    default:
                        break;
                }
                Password = "Sikur@123";
            }
        }
        async Task ExecuteLoginCommandAsync()
        {
            Error = string.Empty;

            // var loginResult = await _loginService.LoginAsync(User, Password).Handle(this);

            Password = string.Empty;

            //if (!loginResult.Success)
            //{
            //    Error = loginResult.Message;
            //    return;
            //}

            //if (!loginResult.Data.Complete)
            //{
            //    if (loginResult.Data.Message == Resource.NeedsEmailConfirmation)
            //    {
            //        var resend = await _dialogService.DisplayAsync(string.Empty, Resource.NeedsEmailConfirmation, Resource.SendEmailAgain, Resource.Ok);
            //        if (resend)
            //        {
            //            var resendResult = await _loginService.ResendEmailConfirmationAsync(User).Handle(this);
            //            if (!resendResult.Success)
            //            {
            //                Error = "Erro no Email";
            //                return;
            //            }
            //        }

            //        return;
            //    }

            //    Error = loginResult.Data.Message;

            //    return;
            //}
            // Preferences.Set(Constants.FirstLauch, true);
            await _navigationService.NavigateAndClearBackStackAsync<CadastroViewModel>();
        }

        async Task ExecuteCadastroCommandAsync()
            => await _navigationService.NavigateAndClearBackStackAsync<CadastroViewModel>();

        #endregion
    }
}
