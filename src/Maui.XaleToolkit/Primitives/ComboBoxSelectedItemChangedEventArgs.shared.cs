using Maui.XaleToolkit.Interfaces;

namespace Maui.XaleToolkit.Primitives
{
    /// <summary>
    /// Event arguments for the SelectedItemChanged event of a <see cref="IComboBox"/> control.
    /// </summary>
    /// <param name="selectedItem">The selected item</param>
    /// <param name="selectedIndex">The index of the selected item</param>
    /// <param name="previousSelection">The previous selected item</param>
    public class ComboBoxSelectedItemChangedEventArgs(object? selectedItem, int selectedIndex, object previousSelection) : EventArgs
    {
        /// <summary>
        /// Gets the currently selected item.
        /// </summary>
        public object? SelectedItem { get; } = selectedItem;

        /// <summary>
        /// Gets the index of the currently selected item.
        /// </summary>
        public int SelectedIndex { get; } = selectedIndex;

        /// <summary>
        /// Gets the previously selected item.
        /// </summary>
        public object PreviousSelection { get; } = previousSelection;
    }
}
