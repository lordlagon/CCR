using Xamarin.Forms;

namespace Core
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty HasErrorProperty =
            BindableProperty.Create(nameof(HasError),
                typeof(bool),
                typeof(CustomEntry));

        public bool HasError
        {
            get => (bool)GetValue(HasErrorProperty);
            set => SetValue(HasErrorProperty, value);
        }

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding),
                typeof(Thickness),
                typeof(CustomEntry));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

    }

    public class CustomEditor : Editor
    {
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding),
                typeof(Thickness),
                typeof(CustomEntry));

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
    }
}
