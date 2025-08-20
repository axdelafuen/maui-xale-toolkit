using Microsoft.Maui.Platform;
using WinTreeView = Microsoft.UI.Xaml.Controls.TreeView;

namespace Maui.XaleToolkit.Platform.TreeView
{
    public partial class MauiTreeView : WinTreeView
    {
        public MauiTreeView() : base() 
        {
            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;
            
            Background = Colors.White.ToPlatform();
            BorderBrush = Colors.Green.ToPlatform();
            BorderThickness = new Microsoft.UI.Xaml.Thickness(5);

            Padding = new Microsoft.UI.Xaml.Thickness(8, 4, 8, 4);

            CornerRadius = new Microsoft.UI.Xaml.CornerRadius(4);
        }
    }
}
