#if IOS || MACCATALYST
using PlatformView = Maui.XaleToolkit.Extensions.ComboBox.MaciosComboBox;
#elif ANDROID
using PlatformView = Maui.XaleToolkit.Extensions.ComboBox.AndroidComboBox;
#elif WINDOWS
using PlatformView = Maui.XaleToolkit.Extensions.ComboBox.WindowsComboBox;
#elif (NETSTANDARD || !PLATFORM) || (NET6_0_OR_GREATER && !IOS && !ANDROID)
using PlatformView = System.Object;
#endif

using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;

namespace Maui.XaleToolkit.Handlers.ComboBox
{
    public partial class ComboBoxHandler
    {
        new private static IPropertyMapper<IComboBox, ComboBoxHandler> ViewMapper = new PropertyMapper<IComboBox, ComboBoxHandler>(ViewHandler.ViewMapper)
        {
            [nameof(IComboBox.ItemsSource)] = MapItemsSource,
            [nameof(IComboBox.SelectedIndex)] = MapSelectedIndex,
            [nameof(IComboBox.SelectedItem)] = MapSelectedItem,
            [nameof(IComboBox.Placeholder)] = MapTitle,
            [nameof(IComboBox.TextColor)] = MapTextColor,
            [nameof(IComboBox.FontSize)] = MapFontSize,
            [nameof(IComboBox.IsEnabled)] = MapIsEnabled,
        };

        new private static CommandMapper<IComboBox, ComboBoxHandler> ViewCommandMapper = new CommandMapper<IComboBox, ComboBoxHandler>(ViewHandler.ViewCommandMapper);

        public ComboBoxHandler() : base(ViewMapper, ViewCommandMapper) { }

        public static void MapItemsSource(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.PlatformView?.UpdateItemsSource();
        }

        public static void MapSelectedIndex(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateSelectedIndex();
        }

        public static void MapSelectedItem(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateSelectedIndex();
        }

        public static void MapTitle(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateTitle();
        }

        public static void MapTextColor(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateTextColor();
        }

        public static void MapFontSize(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateFontSize();
        }

        public static void MapIsEnabled(ComboBoxHandler handler, IComboBox spinner)
        {
            handler.UpdateIsEnabled();
        }

    }
}
