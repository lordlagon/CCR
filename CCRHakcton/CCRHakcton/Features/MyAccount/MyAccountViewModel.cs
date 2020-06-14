using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Core
{
    public class MyAccountViewModel : BaseItemViewModel<MyAccountWrapper>
    {
        #region Services
        readonly IDialogService _dialogService;
        readonly IValidationService _validationService;
        #endregion

        #region Commands
        public ICommand AvatarOptionSelectedCommand { get; }
        public ICommand SaveAccountCommand { get; }
        #endregion

        #region Properties
        bool _forceSync = false;
        string _name;
        string _email;
        #endregion

        #region Init
        public MyAccountViewModel(INavigationService navigationService,
                                  IDialogService dialogService,
                                  IValidationService validationService) : base(navigationService)
        {
            _dialogService = dialogService;
            _validationService = validationService;

            AvatarOptionSelectedCommand = new Command(async () => await ExecuteAvatarOptionSelectedCommandAsync());
            SaveAccountCommand = new Command<bool>(async (email) => await ExecuteSaveAccountCommandAsync(email));
        }
        #endregion

        #region ExecuteCommands
        async Task ExecuteAvatarOptionSelectedCommandAsync()
        {
            var buttons = new string[] { "Tirar Foto", "Pegar Imagem" };

            var action = await _dialogService.DisplayAsync("Alterar Foto", "Cancelar", null, buttons);

            await CrossMedia.Current.Initialize();
            MediaFile file = null;
            switch ((PhotoOption)action)
            {
                case PhotoOption.Delete:
                    //var deletedResult = await _myAccountService.DeleteAvatarAsync().Handle(this);
                    //if (!deletedResult.Success)
                    //{
                    //    await _dialogService.DisplayAsync(deletedResult.Message, "Ok");
                    //    return;
                    //}

                    //if (!deletedResult.Data.Complete)
                    //{
                    //    await _dialogService.DisplayAsync(deletedResult.Data.Message, "Ok");
                    //    return;
                    //}

                    await UpdateDataAsync();
                    return;
                case PhotoOption.Pick:
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await _dialogService.DisplayAsync("Não Suporta Galeria", "Voltar");
                        return;
                    }
                    file = await CrossMedia.Current.PickPhotoAsync();

                    if (file == null)
                        return;

                    break;
                case PhotoOption.Take:
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await _dialogService.DisplayAsync("Camera não Detectada", "Voltar");
                        return;
                    }

                    file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        SaveToAlbum = false,
                        MaxWidthHeight = 600,
                        RotateImage = false,
                        AllowCropping = true,
                        CompressionQuality = 20,
                        Name = "userProfile.jpg",
                        Directory = "skphoto"
                    });

                    if (file == null)
                        return;

                    break;
                case PhotoOption.Cancel:
                    return;

            }

          //  var result = await _myAccountService.UpdateAccountAsync(file).Handle(this);

            //if (!result.Success)
            //{
            //    await _dialogService.DisplayAsync(result.Message, Resource.Back);
            //    return;
            //}

            //if (!result.Data.Complete)
            //{
            //    await _dialogService.DisplayAsync(result.Data.Message, Resource.Back);
            //    return;
            //}

            await UpdateDataAsync();
        }

        async Task ExecuteSaveAccountCommandAsync(bool email)
        {
            if (_name == Item.Name && _email == Item.Email)
                return;

            if (string.IsNullOrEmpty(Item.Email) || !_validationService.EmailValid(Item.Email))
            {
              //  await _dialogService.DisplayAsync(Resource.EmailInvalid, Resource.Ok);
                return;
            }

            //if (email)
            //    await _dialogService.DisplayAsync(string.Format(Resource.SendConfirmEmail, Item.Email), Resource.Ok);
            //var result = await _myAccountService.UpdateAccountAsync(Item.Name, Item.Email).Handle(this);
            //if (!result.Data.Complete)
            //    await _dialogService.DisplayAsync(result.Data.Message, Resource.Ok);
            //await UpdateDataAsync();
        }
        #endregion

        #region LoadData
        protected override Task<MyAccountWrapper> GetDataAsync()
        {
            var user = new MyAccountWrapper
            {
                //Name = "Fulano",
                //Email = "Fulano@gmail.com", 
                //CNH =   "012567-4",
                //CPF = "000.000.000-00",
                //Sexo = "M",
                //Telefone = "(41) 3333-0000"
            };
            return Task.FromResult(user);  // => _myAccountService.GetUserAsync(_forceSync);
        }
        
        #endregion

        #region Aux
        async Task UpdateDataAsync()
        {
            _forceSync = true;
            await RefreshDataAsync();
            MessagingCenter.Send(this, Constants.UpdateUser);
        }
        #endregion
    }
}
