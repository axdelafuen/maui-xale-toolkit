#if ANDROID
using Android.Content;
using Android.Util;
using AndroidX.AppCompat.Widget;

namespace Maui.XaleToolkit.Extensions.ComboBox
{
    public class AndroidComboBox : AppCompatSpinner
    {
        private static Context GetContext()
        {
            return Android.App.Application.Context ?? throw new InvalidOperationException("Context cannot be null");
        }

        public AndroidComboBox() : base(GetContext(), null, Android.Resource.Attribute.SpinnerStyle, (int)Android.Widget.SpinnerMode.Dropdown)
        {
            Focusable = true;
            SetBackgroundResource(Android.Resource.Drawable.SpinnerBackground);
        }
        public AndroidComboBox(Context context) : base(context) { }
        public AndroidComboBox(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public AndroidComboBox(Context context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

        public AndroidComboBox(Context context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

        public override void SetSelection(int position)
        {
            bool sameSelected = position == SelectedItemPosition;
            base.SetSelection(position);
            if (sameSelected)
            {
                OnItemSelectedListener?.OnItemSelected(this, SelectedView, position, SelectedItemId);
            }
        }

        public override void SetSelection(int position, bool animate)
        {
            bool sameSelected = position == SelectedItemPosition;
            base.SetSelection(position, animate);
            if (sameSelected)
            {
                OnItemSelectedListener?.OnItemSelected(this, SelectedView, position, SelectedItemId);
            }
        }
    }
}
#endif