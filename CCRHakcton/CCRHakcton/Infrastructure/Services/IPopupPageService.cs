using Xamarin.Forms;

namespace Core
{
    public interface IPopupPageService
    {
        Page Page { get; }
        void Show(Page page);
        void Hide();
    }
}
