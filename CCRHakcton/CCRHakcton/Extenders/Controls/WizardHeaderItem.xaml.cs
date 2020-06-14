
using Xamarin.Forms;

namespace Core
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public partial class WizardHeaderItem : StackLayout
    {
        public static readonly BindableProperty ShowLineProperty = BindableProperty.Create(
          nameof(ShowLine), typeof(bool), typeof(WizardHeaderItem), false);

        public bool ShowLine
        {
            get => (bool)GetValue(ShowLineProperty);
            set => SetValue(ShowLineProperty, value);
        }


        public static readonly BindableProperty TextProperty = BindableProperty.Create(
         nameof(Text), typeof(string), typeof(WizardHeaderItem));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ActiveProperty = BindableProperty.Create(
          nameof(Active), typeof(bool), typeof(WizardHeaderItem), false);

        public bool Active
        {
            get => (bool)GetValue(ActiveProperty);
            set => SetValue(ActiveProperty, value);
        }

        public WizardHeaderItem()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}