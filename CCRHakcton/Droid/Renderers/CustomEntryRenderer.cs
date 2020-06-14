using System.ComponentModel;
using Android.Content;
using Core;
using Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                SetControlBackground();
                SetPadding();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(CustomEntry.HasError))
                SetControlBackground();

            if (e.PropertyName == nameof(CustomEntry.Padding))
                SetPadding();
        }

        void SetPadding()
        {
            var customEntry = (Element as CustomEntry);
            Control.SetPadding(GetDP(customEntry.Padding.Left),
                               GetDP(customEntry.Padding.Top),
                               GetDP(customEntry.Padding.Right),
                               GetDP(customEntry.Padding.Bottom));
        }

        int GetDP(double value)
           => (int)(value * Resources.DisplayMetrics.Density + 0.5f);

        void SetControlBackground()
        {
            if ((Element as CustomEntry).HasError)
                Control.Background = Context.GetDrawable(Resource.Drawable.CustomEntryError);
            else
                Control.Background = Context.GetDrawable(Resource.Drawable.CustomEntry);
        }
    }
}