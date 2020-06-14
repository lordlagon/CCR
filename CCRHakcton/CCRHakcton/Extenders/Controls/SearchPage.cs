using System;
using Xamarin.Forms;

namespace Core
{
    public abstract class SearchPage : ContentPage, ISearchPage
    {
        public SearchPage()
        {
            SearchBarTextChanged += HandleSearchBarTextChanged;
        }

        public event EventHandler<string> SearchBarTextChanged;

        string _filter = string.Empty;

        public void OnSearchBarTextChanged(in string text)
        {
            if (_filter == text)
                return;

            _filter = text;
            SearchBarTextChanged?.Invoke(this, text);
        }
        public virtual void HandleSearchBarTextChanged(object sender, string searchBarText) { }
    }
}
