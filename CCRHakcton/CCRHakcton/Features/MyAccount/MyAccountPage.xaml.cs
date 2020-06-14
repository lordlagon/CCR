using Xamarin.Forms;

namespace Core
{
    public partial class MyAccountPage : BackToHomePage
    {
        MyAccountViewModel ViewModel => BindingContext as MyAccountViewModel;

        public MyAccountPage()
            => InitializeComponent();
        
        public void EntryNome_UnFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
            => ViewModel.SaveAccountCommand.Execute(false);
        public void EntryEmail_UnFocused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
            => ViewModel.SaveAccountCommand.Execute(true);
    }
}
