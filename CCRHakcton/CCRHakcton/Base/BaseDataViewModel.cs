using System.Threading.Tasks;

namespace Core
{
    public abstract class BaseDataViewModel<T> : BaseViewModel where T : class
    {
        bool _dataLoaded;
        public bool DataLoaded
        {
            get { return _dataLoaded; }
            private set { SetProperty(ref _dataLoaded, value); }
        }
        protected BaseDataViewModel(INavigationService navigationService, string title) : base(navigationService, title)
            => Init();

        protected BaseDataViewModel(INavigationService navigationService) : base(navigationService)
            => Init();

        void Init()
        {
            DataLoaded = false;
        }
        public override Task InitializeAsync(object navigationData)
            => InitializeAsync();

        protected override Task InitializeAsync()
            => LoadDataAsync();

        protected abstract Task<T> GetDataAsync();
        protected abstract Task SetDataLoadedAsync(T data);
        protected virtual Task OnDataLoadedAsync()
            => Task.FromResult(DataLoaded = true);
        protected virtual Task OnDataLoadErrorAsync(ApiResult<T> result)
            => Task.FromResult(true);
        public Task RefreshDataAsync()
            => LoadDataAsync();
        async Task LoadDataAsync()
        {
            IsBusy = true;
            DataLoaded = false;
            var result = await GetDataAsync().Handle();
            if (result?.Data == null || result?.Success == false)
            {
                IsBusy = false;
                await OnDataLoadErrorAsync(result);
                return;
            }
            await SetDataLoadedAsync(result.Data);
            await OnDataLoadedAsync();
            IsBusy = false;
        }
    }
}
