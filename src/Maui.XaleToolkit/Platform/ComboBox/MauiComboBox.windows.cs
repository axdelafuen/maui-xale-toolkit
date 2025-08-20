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
    }
}
