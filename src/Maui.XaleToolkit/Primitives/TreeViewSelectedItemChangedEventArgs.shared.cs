using Maui.XaleToolkit.Interfaces;

namespace Maui.XaleToolkit.Primitives
{
    /// <summary>
    /// Event arguments for the SelectedItemChanged event of a <see cref="ITreeView"/> control.
    /// </summary>
    /// <param name="selectedItem">The selected item</param>
    public class TreeViewSelectedItemChangedEventArgs(object? selectedItem) : EventArgs
    {
        /// <summary>
        /// Gets the currently selected item.
        /// </summary>
        public object? SelectedItem { get; } = selectedItem;
    }
}
