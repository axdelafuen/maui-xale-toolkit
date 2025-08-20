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

        private static void MapItemsSource(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateItemsSource();
        private static void MapSelectedIndex(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateSelectedIndex();
        private static void MapSelectedItem(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateSelectedIndex();
        private static void MapTitle(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateTitle();
        private static void MapTextColor(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateTextColor();
        private static void MapFontSize(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateFontSize();
        private static void MapIsEnabled(ComboBoxHandler handler, IComboBox spinner) => handler.UpdateIsEnabled();
    }
}
