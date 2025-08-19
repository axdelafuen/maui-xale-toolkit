using UIKit;
using Foundation;

namespace Maui.XaleToolkit.Platform.ComboBox
{
    public class MauiComboBox : UIButton
    {
        private UIPickerView? _pickerView;
        private UIToolbar? _toolbar;
        private NSArray? _dataSource;
        
        public MauiComboBox()
        {
            SetupPicker();
            SetupAppearance();
        }
        
        private void SetupPicker()
        {
            _pickerView = new UIPickerView();
            _toolbar = new UIToolbar();
        }
        
        private void SetupAppearance()
        {
            Layer.BorderWidth = 1;
            Layer.BorderColor = UIColor.Gray.CGColor;
            Layer.CornerRadius = 5;
        }
    }
}
