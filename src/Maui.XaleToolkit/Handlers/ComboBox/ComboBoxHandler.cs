using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;

namespace Maui.XaleToolkit.Handlers.ComboBox
{
    public partial class ComboBoxHandler
    {
        new private readonly static IPropertyMapper<IComboBox, ComboBoxHandler> ViewMapper = new PropertyMapper<IComboBox, ComboBoxHandler>(ViewHandler.ViewMapper)
        {
            [nameof(IComboBox.ItemsSource)] = MapItemsSource,
            [nameof(IComboBox.SelectedIndex)] = MapSelectedIndex,
            [nameof(IComboBox.SelectedItem)] = MapSelectedItem,
            [nameof(IComboBox.Placeholder)] = MapTitle,
            [nameof(IComboBox.TextColor)] = MapTextColor,
            [nameof(IComboBox.FontSize)] = MapFontSize,
            [nameof(IComboBox.IsEnabled)] = MapIsEnabled,
        };

        new private readonly static CommandMapper<IComboBox, ComboBoxHandler> ViewCommandMapper = new CommandMapper<IComboBox, ComboBoxHandler>(ViewHandler.ViewCommandMapper);

        public ComboBoxHandler() : base(ViewMapper, ViewCommandMapper) { }

        public static void MapItemsSource(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateItemsSource();
        public static void MapSelectedIndex(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateSelectedIndex();
        public static void MapSelectedItem(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateSelectedIndex();
        public static void MapTitle(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateTitle();
        public static void MapTextColor(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateTextColor();
        public static void MapFontSize(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateFontSize();
        public static void MapIsEnabled(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateIsEnabled();
    }
}
