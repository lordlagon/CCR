using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Essentials = Xamarin.Essentials;

namespace Core
{
    public class ParadaDetailViewModel : BaseItemViewModel<CupomWrapper>
    {
        #region Services
        readonly IDialogService _dialogService;
        #endregion

        #region Properties
        bool _isGroup;
        public bool IsGroup
        {
            get => _isGroup;
            set => SetProperty(ref _isGroup, value);
        }

        string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }


        bool _recordProcess;
        public bool RecordProcess
        {
            get => _recordProcess;
            set => SetProperty(ref _recordProcess, value);
        }

        string _timeRecorded;
        public string TimeRecorded
        {
            get => _timeRecorded;
            set => SetProperty(ref _timeRecorded, value);
        }

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        #endregion

        #region Commands
        public ICommand SendFileCommand { get; }
        public ICommand SendImageCommand { get; }
        public ICommand OpenImageCommand { get; }
        public ICommand SendTextCommand { get; }
        public ICommand ItemOptionsCommand { get; }
        public ICommand SendMessageCommand { get; }
        public ICommand SendMessageMediaCommand { get; }
        public ICommand PlayPauseCommand { get; }
        public ICommand SelectMessageCommand { get; }
        public Action ScrollToEnd { get; set; }
        public ICommand LoadOldestMessages { get; }
        public ICommand ContactDetailCommand { get; }
        public ICommand DeleteConversationCommand { get; }

        public ICommand DeleteGroupCommand { get; }
        public ICommand LeaveGroupCommand { get; }
        public ICommand EditGroupCommand { get; }

        public ICommand MenuCommand { get; }

        #endregion

        #region Init
        public ParadaDetailViewModel(INavigationService navigationService, IDialogService dialogService) : base(navigationService)
        {
            _dialogService = dialogService;

            
        }


        #endregion

        public override Task NavigationBackAsync(object parameter)
        {
            return base.NavigationBackAsync(parameter);
        }

        #region ExecuteCommand


        #endregion

        #region GetData

        
        protected async override Task OnDataLoadedAsync()
        {
            await base.OnDataLoadedAsync();
            ScrollToEnd?.Invoke();
        }

        protected override Task<CupomWrapper> GetDataAsync()
        {
            return Task.FromResult(new CupomWrapper
            {
                Title = "Posto Costa Brava",
                Description = "Rodovia Régis Bittencourt",
                Numero = "Km 67",
                Cep = "83420000",
                Rating = 4,
                Telefone = "4136721683"
            });
        }
        #endregion
    }
}