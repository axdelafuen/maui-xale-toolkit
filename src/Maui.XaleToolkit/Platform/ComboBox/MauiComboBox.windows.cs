using Microsoft.Maui.Platform;
using WinComboBox = Microsoft.UI.Xaml.Controls.ComboBox;

namespace Maui.XaleToolkit.Platform.ComboBox
{
    /// <summary>
    /// Windows implementation of a ComboBox control.
    /// </summary>
    public partial class MauiComboBox : WinComboBox
    {
        /// <summary>
        /// Default constructor for Windows <see cref="MauiComboBox"/> implementation.
        /// </summary>
        public MauiComboBox() : base()
        {
            DefaultStyleKey = typeof(WinComboBox);

            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;

            Background = Colors.White.ToPlatform();
            BorderBrush = Colors.Gray.ToPlatform();
            BorderThickness = new Microsoft.UI.Xaml.Thickness(1);

            Padding = new Microsoft.UI.Xaml.Thickness(8, 4, 8, 4);

            CornerRadius = new Microsoft.UI.Xaml.CornerRadius(4);
        }

        /// <summary>
        /// Ensures the same item can be selected again (similar to AlwaysFireSpinner on Android)
        /// </summary>
        public void ForceSelectionChanged()
        {
            if (SelectedIndex >= 0)
            {
                var currentIndex = SelectedIndex;
                SelectedIndex = -1;
                SelectedIndex = currentIndex;
            }
        }

        /// <summary>
        /// Sets the placeholder text when no item is selected
        /// </summary>
        /// <param name="placeholder">The placeholder text</param>
        public void SetPlaceholder(string placeholder)
        {
            if (string.IsNullOrEmpty(placeholder))
                return;

            PlaceholderText = placeholder;
        }

        /// <summary>
        /// Updates the visual state based on enabled/disabled state
        /// </summary>
        /// <param name="isEnabled">Whether the control is enabled</param>
        public void UpdateEnabledState(bool isEnabled)
        {
            IsEnabled = isEnabled;
            Opacity = isEnabled ? 1.0 : 0.5;

            if (isEnabled)
                Background = Colors.White.ToPlatform();
            else
                Background = Colors.LightGray.ToPlatform();
        }

        /// <summary>
        /// Applies custom styling to match the app theme
        /// </summary>
        /// <param name="textColor">Text color</param>
        /// <param name="fontSize">Font size</param>
        public void ApplyCustomStyling(Windows.UI.Color textColor, double fontSize)
        {
            Foreground = textColor.ToColor().ToPlatform();
            FontSize = fontSize;
        }
    }
}
