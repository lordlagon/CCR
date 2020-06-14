using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Core
{
    public abstract class BasePaginatedViewModel<T> : BaseListViewModel<T> where T : class
    {
        bool _hasMore = true;

        string _filter;
        public string Filter
        {
            get { return _filter; }
            set { SetProperty(ref _filter, value); }
        }

        protected int CurrentPage { get; private set; }

        public ICommand LoadMoreCommand { get; private set; }

        public ICommand FilterChangedCommand { get; private set; }

        protected BasePaginatedViewModel(INavigationService navigationService, string title) : base(navigationService, title)
            => Ctor();

        protected BasePaginatedViewModel(INavigationService navigationService) : base(navigationService)
            => Ctor();

        void Ctor()
        {
            LoadMoreCommand = new Command(async () => await ExecuteLoadMoreCommandAsync());
            FilterChangedCommand = new Command(async () => await ExecuteFilterChangedCommandAsync());
        }

        async Task ExecuteLoadMoreCommandAsync()
        {
            if (_hasMore)
            {
                CurrentPage += 1;
                await RefreshDataAsync();
            }
        }

        protected override async Task SetDataLoadedAsync(IEnumerable<T> data)
        {
            _hasMore = data?.Any() == true;
            if (_hasMore)
            {
                data.ForEach(item => Items.Add(item));
                await Task.Delay(250);
            }
        }

        protected virtual async Task ExecuteFilterChangedCommandAsync()
            => await CleanAndRefreshDataAsync();       

        async Task CleanAndRefreshDataAsync()
        {
            CurrentPage = 0;
            _hasMore = true;
            Items.Clear();
            await RefreshDataAsync();
        }
    }
}
