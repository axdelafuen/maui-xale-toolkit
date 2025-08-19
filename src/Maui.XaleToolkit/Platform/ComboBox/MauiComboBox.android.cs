using Android.Content;
using Android.Util;
using AndroidX.AppCompat.Widget;

namespace Maui.XaleToolkit.Platform.ComboBox
{
    /// <summary>
    /// Android implementation of a ComboBox control.
    /// </summary>
    public class MauiComboBox : AppCompatSpinner
    {
        /// <summary>
        /// Constructor for Android <see cref="MauiComboBox"/> implementation.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        public MauiComboBox(Context context) : base(context) { }

        /// <summary>
        /// Constructor for Android <see cref="MauiComboBox"/> implementation.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        /// <param name="attrs">The <see cref="IAttributeSet"/> containing the attributes.</param>
        public MauiComboBox(Context context, IAttributeSet attrs) : base(context, attrs) { }

        /// <summary>
        /// Constructor for Android <see cref="MauiComboBox"/> implementation.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        /// <param name="attrs">The <see cref="IAttributeSet"/> containing the attributes.</param>
        /// <param name="defStyleAttr">The default style attribute.</param>
        public MauiComboBox(Context context, IAttributeSet? attrs, int defStyleAttr) : base(context, attrs, defStyleAttr) { }

        /// <summary>
        /// Constructor for Android <see cref="MauiComboBox"/> implementation.
        /// </summary>
        /// <param name="context">The current <see cref="Context"/>.</param>
        /// <param name="attrs">The <see cref="IAttributeSet"/> containing the attributes.</param>
        /// <param name="defStyleAttr">The default style attribute.</param>
        /// <param name="defStyleRes">The default style resource.</param>
        public MauiComboBox(Context context, IAttributeSet? attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes) { }

        /// <summary>
        /// Sets the selection of the ComboBox and force to notify the listener, even if the selection is the same as the current selection.
        /// </summary>
        /// <param name="position">The index of the selected item.</param>
        public override void SetSelection(int position)
        {
            bool sameSelected = position == SelectedItemPosition;
            base.SetSelection(position);
            if (sameSelected)
            {
                OnItemSelectedListener?.OnItemSelected(this, SelectedView, position, SelectedItemId);
            }
        }

        /// <summary>
        /// Sets the selection of the ComboBox and force to notify the listener, even if the selection is the same as the current selection.
        /// </summary>
        /// <param name="position">The index of the selected item.</param>
        /// <param name="animate">Whether to animate the selection change.</param>
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
