using Android.Content;
using Android.Util;
using AndroidX.AppCompat.Widget;

namespace Maui.XaleToolkit.Platform.ComboBox
{
    public class MauiComboBox : AppCompatSpinner
    {
        public MauiComboBox(Context context) : base(context) { }
        public MauiComboBox(Context context, IAttributeSet attrs) : base(context, attrs) { }
        public MauiComboBox(Context context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

        public MauiComboBox(Context context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

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
