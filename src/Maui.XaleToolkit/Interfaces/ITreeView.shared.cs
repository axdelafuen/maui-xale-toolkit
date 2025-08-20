using Maui.XaleToolkit.Primitives;
using System.Collections;

namespace Maui.XaleToolkit.Interfaces
{
    public interface ITreeView : IView
    {
        /// <summary>
        /// Collection of root nodes for the TreeView.
        /// </summary>
        IList ItemsSource { get; set; }

        /// <summary>
        /// The currently selected item.
        /// </summary>
        object? SelectedItem { get; set; }
        
        /// <summary>
        /// Text color of the control
        /// </summary>
        Color TextColor { get; set; }

        /// <summary>
        /// Font size of the control
        /// </summary>
        double FontSize { get; set; }

        /// <summary>
        /// Event triggered when selection changes.
        /// </summary>
        event EventHandler<TreeViewSelectedItemChangedEventArgs> SelectedItemChanged;
    }
}
