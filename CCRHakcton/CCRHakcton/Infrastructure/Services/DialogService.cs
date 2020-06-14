using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Core
{
    public interface IDialogService
    {
        Task<bool> DisplayAsync(string title, string message, string ok, string cancel);
        Task DisplayAsync(string message, string cancel);
        Task DisplayAsync(string title, string message, string cancel);
        Task<int> DisplayAsync(string title, string cancel, string destruction, string[] buttons);
    }

    public class DialogService : IDialogService
    {       
        public Task<bool> DisplayAsync(string title, string message, string ok, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        public Task DisplayAsync(string title, string message, string cancel) =>
            Application.Current.MainPage.DisplayAlert(title, message, cancel);
        public Task DisplayAsync(string message, string cancel)
            => DisplayAsync(string.Empty, message, cancel);
        public async Task<int> DisplayAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            var button = await Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
            var i = Array.IndexOf<string>(buttons, button);
            return i;
        }
    }
}
