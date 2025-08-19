using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Media;
using WinComboBox = Microsoft.UI.Xaml.Controls.ComboBox;

namespace Maui.XaleToolkit.Platform.ComboBox
{
    public partial class MauiComboBox : WinComboBox
    {
        public MauiComboBox() : base()
        {
            DefaultStyleKey = typeof(WinComboBox);

            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;

            MinWidth = 120;
            MinHeight = 32;

            MaxDropDownHeight = 200;

            FontFamily = new FontFamily("Segoe UI");
            FontSize = 14;

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
                var currentItem = SelectedItem;

                // Temporarily change selection to force event
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

            // If no selection, show placeholder as header
            if (SelectedIndex == -1)
            {
                Header = placeholder;
            }
        }

        /// <summary>
        /// Updates the visual state based on enabled/disabled state
        /// </summary>
        /// <param name="isEnabled">Whether the control is enabled</param>
        public void UpdateEnabledState(bool isEnabled)
        {
            IsEnabled = isEnabled;
            Opacity = isEnabled ? 1.0 : 0.5;

            // Update visual state
            if (isEnabled)
            {
                Background = Colors.White.ToPlatform();
            }
            else
            {
                Background = Colors.LightGray.ToPlatform();
            }
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
