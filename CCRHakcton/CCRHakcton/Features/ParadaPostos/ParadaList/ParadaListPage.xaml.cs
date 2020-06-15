using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Core
{
    public partial class ParadaListPage : ContentPage
    {
        ParadaListViewModel ViewModel => BindingContext as ParadaListViewModel;

        public ParadaListPage()
        {
            InitializeComponent();
        }
        void ListView_PropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ViewModel != null && ViewModel.Items != null)
                ViewModel.Items.OrderBy(o => o.Title);
        }
    }
}