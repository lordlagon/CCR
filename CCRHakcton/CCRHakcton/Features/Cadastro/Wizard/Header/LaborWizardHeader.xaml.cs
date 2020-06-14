using System.Linq;
using Xamarin.Forms;

namespace Core
{
    [Xamarin.Forms.Internals.Preserve(AllMembers = true)]
    public partial class LaborWizardHeader : StackLayout
    {
        public static readonly BindableProperty CurrentStepProperty = BindableProperty.Create(
                  nameof(CurrentStep), typeof(int), typeof(LaborWizardHeader), 0, propertyChanged:
                    (bindable, oldValue, newValue) => {
                        if (bindable is LaborWizardHeader view)
                        {
                            var items = view.Children.Where(w => w is WizardHeaderItem);
                            for (var i = 0; i < items.Count(); i++)
                            {
                                (items.ElementAt(i) as WizardHeaderItem).Active = i <= view.CurrentStep - 1;
                            }
                        }
                    });

        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        public LaborWizardHeader()
        {
            InitializeComponent();
        }
    }
}