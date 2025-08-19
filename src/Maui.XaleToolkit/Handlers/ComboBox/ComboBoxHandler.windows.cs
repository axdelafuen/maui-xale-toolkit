using Microsoft.Maui.Handlers;
using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Platform.ComboBox;
using Microsoft.Maui.Platform;

namespace Maui.XaleToolkit.Handlers.ComboBox
{
    public partial class ComboBoxHandler : ViewHandler<IComboBox, MauiComboBox>
    {
        private bool _isUpdatingSelection = false;

        protected override MauiComboBox CreatePlatformView()
        {
            var comboBox = new MauiComboBox();

            // Set up basic styling
            comboBox.HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch;
            comboBox.VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center;

            return comboBox;
        }

        protected override void ConnectHandler(MauiComboBox platformView)
        {
            base.ConnectHandler(platformView);

            if (VirtualView != null)
            {
                platformView.SelectionChanged += OnSelectionChanged; ;
                platformView.DropDownOpened += OnDropDownOpened;

                // Initialize all properties
                UpdateItemsSource();
                UpdateSelectedIndex();
                UpdateTitle();
                UpdateTextColor();
                UpdateFontSize();
                UpdateIsEnabled();
            }
        }

        protected override void DisconnectHandler(MauiComboBox platformView)
        {
            if (platformView != null)
            {
                platformView.SelectionChanged -= OnSelectionChanged;
                platformView.DropDownOpened -= OnDropDownOpened;
            }

            base.DisconnectHandler(platformView);
        }

        private void OnSelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            if (_isUpdatingSelection || VirtualView == null)
                return;

            try
            {
                var selectedIndex = PlatformView.SelectedIndex;
                var selectedItem = PlatformView.SelectedItem;

                if (selectedIndex >= 0 && VirtualView.ItemsSource != null && selectedIndex < VirtualView.ItemsSource.Count)
                {
                    VirtualView.SelectedIndex = selectedIndex;
                    VirtualView.SelectedItem = VirtualView.ItemsSource[selectedIndex];
                }
                else
                {
                    VirtualView.SelectedIndex = -1;
                    VirtualView.SelectedItem = null;
                }
            }
            catch (Exception) { }
        }

        private void OnDropDownOpened(object sender, object e)
        {
        }

        #region IComboBoxHandler Implementation

        public void UpdateItemsSource()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                PlatformView.Items.Clear();

                if (VirtualView.ItemsSource != null)
                {
                    foreach (var item in VirtualView.ItemsSource)
                    {
                        PlatformView.Items.Add(item?.ToString() ?? string.Empty);
                    }
                }

                UpdateTitle();
            }
            catch (Exception) { }
        }

        public void UpdateSelectedIndex()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            _isUpdatingSelection = true;
            try
            {
                var selectedIndex = VirtualView.SelectedIndex;

                if (selectedIndex >= 0 && selectedIndex < PlatformView.Items.Count)
                {
                    PlatformView.SelectedIndex = selectedIndex;
                }
                else
                {
                    PlatformView.SelectedIndex = -1;
                }
            }
            catch (Exception) { }
            finally
            {
                _isUpdatingSelection = false;
            }
        }

        public void UpdateSelectedItem()
        {
            // Windows ComboBox handles this through UpdateSelectedIndex
            UpdateSelectedIndex();
        }

        public void UpdateTitle()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                // Set placeholder text when no item is selected
                PlatformView.PlaceholderText = VirtualView.Placeholder ?? string.Empty;

                // If no selection and placeholder exists, show placeholder
                if (VirtualView.SelectedIndex == -1 && !string.IsNullOrEmpty(VirtualView.Placeholder))
                {
                    PlatformView.Header = VirtualView.Placeholder;
                }
                else
                {
                    PlatformView.Header = null;
                }
            }
            catch (Exception) { }
        }

        public void UpdateTextColor()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                var color = VirtualView.TextColor;
                if (color != null)
                {
                    PlatformView.Foreground = color.ToPlatform();
                }
            }
            catch (Exception) { }
        }

        public void UpdateFontSize()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                if (VirtualView.FontSize > 0)
                {
                    PlatformView.FontSize = VirtualView.FontSize;
                }
            }
            catch (Exception) { }
        }

        public void UpdateIsEnabled()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                PlatformView.IsEnabled = VirtualView.IsEnabled;

                // Adjust opacity for disabled state
                PlatformView.Opacity = VirtualView.IsEnabled ? 1.0 : 0.5;
            }
            catch (Exception) { }
        }

        #endregion
    }
}
