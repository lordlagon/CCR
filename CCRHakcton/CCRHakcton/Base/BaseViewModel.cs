using System;
using System.Threading.Tasks;

namespace Core
{
    public interface IBaseViewModel : IDisposable
    {
        Task InitializeAsync(object navigationData);
        Task NavigationBackAsync(object parameter);
    }

    public class BaseViewModel : BindingObject, IBaseViewModel
    {
        protected readonly INavigationService _navigationService;
       // protected ILoadingPageService _loadingPageService => App.Instance.Container.Resolve<ILoadingPageService>();

        private string _title;
        public string Title
        {
            get => _title;
            protected set => SetProperty(ref _title, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (value == _isBusy)
                    return;

                //if (value)
                //  _loadingPageService.ShowLoadingPage();
                //else
                //    _loadingPageService.HideLoadingPage();
                
                SetProperty(ref _isBusy, value);
            }
        }

        protected BaseViewModel(INavigationService navigationService, string title)
        {
            _navigationService = navigationService;
            Title = title;
        }

        protected BaseViewModel(INavigationService navigationService)
            => _navigationService = navigationService;

        public virtual Task InitializeAsync(object navigationData)
            => InitializeAsync();

        protected virtual Task InitializeAsync()
            => Task.FromResult(true);

        public virtual Task NavigationBackAsync(object parameter)
            => Task.FromResult(true);

        public virtual void Dispose() { }
    }
}
