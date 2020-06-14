using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Core
{
    public static class AppConfiguration
    {
        public static string ApiUrl => "https://api-dev.pbd.com";
        public static string AppCenterKey => "android=2bbe4fc2-5bd2-4f97-8c2f-7056bdb3afc4;";

        public static async Task Initialize()
            => StoreService.Start(
#if DEBUG
                     await Task.FromResult(Preferences.Get(Constants.PinKey, string.Empty))
#else
                        await SecureStorage.GetAsync(Constants.PinKey)
#endif
                );
    }
}