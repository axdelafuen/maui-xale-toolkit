#if WINDOWS
using Microsoft.UI.Xaml;
using WinComboBox = Microsoft.UI.Xaml.Controls.ComboBox;

namespace Maui.XaleToolkit.Extensions.ComboBox
{
    public partial class WindowsComboBox : WinComboBox
    {
        public WindowsComboBox()
        {
            SetupAppearance();
        }
        
        private void SetupAppearance()
        {
            //HorizontalAlignment = HorizontalAlignment.Stretch;
            //VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
#endif