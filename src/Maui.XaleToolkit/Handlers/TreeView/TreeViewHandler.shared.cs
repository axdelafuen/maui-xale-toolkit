using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    /// <summary>
    /// Handler for the <see cref="ITreeView"/> control.
    /// </summary>
    public partial class TreeViewHandler
    {
        new private readonly static IPropertyMapper<ITreeView, TreeViewHandler> ViewMapper = new PropertyMapper<ITreeView, TreeViewHandler>(ViewHandler.ViewMapper)
        {
            [nameof(ITreeView.ItemsSource)] = MapItemsSource,
            [nameof(ITreeView.SelectedItem)] = MapSelectedItem,
            [nameof(ITreeView.TextColor)] = MapTextColor,
            [nameof(ITreeView.FontSize)] = MapFontSize,
        };
        
        new private readonly static CommandMapper<ITreeView, TreeViewHandler> ViewCommandMapper = new CommandMapper<ITreeView, TreeViewHandler>(ViewHandler.ViewCommandMapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewHandler"/> class.
        /// </summary>
        public TreeViewHandler() : base(ViewMapper, ViewCommandMapper) { }

        private static void MapItemsSource(TreeViewHandler handler, ITreeView spinner) => handler.UpdateItemsSource();
        private static void MapSelectedItem(TreeViewHandler handler, ITreeView spinner) => handler.UpdateSelectedItem();
        private static void MapTextColor(TreeViewHandler handler, ITreeView spinner) => handler.UpdateTextColor();
        private static void MapFontSize(TreeViewHandler handler, ITreeView spinner) => handler.UpdateFontSize();
    }
}
