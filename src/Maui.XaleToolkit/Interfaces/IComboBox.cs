using System.Collections;

namespace Maui.XaleToolkit.Interfaces
{
    /// <summary>
    /// Interface for a <see cref="IComboBox"/> control.
    /// </summary>
    public interface IComboBox : IView
    {
        /// <summary>
        /// Collection of items to display
        /// </summary>
        IList ItemsSource { get; set; }

        /// <summary>
        /// Currently selected item
        /// </summary>
        object? SelectedItem { get; set; }

        /// <summary>
        /// Index of the currently selected item
        /// </summary>
        int SelectedIndex { get; set; }

        /// <summary>
        /// Title/hint text shown when no item is selected
        /// </summary>
        string Placeholder { get; set; }

        /// <summary>
        /// Text color of the control
        /// </summary>
        Color TextColor { get; set; }

        /// <summary>
        /// Font size of the control
        /// </summary>
        double FontSize { get; set; }

        /// <summary>
        /// Whether the control is enabled
        /// </summary>
        new bool IsEnabled { get; set; }

        /// <summary>
        /// Event fired when selection changes
        /// </summary>
        event EventHandler<ComboBoxSelectionChangedEventArgs> SelectionChanged;
    }

    /// <summary>
    /// Event arguments for the SelectionChanged event of a <see cref="IComboBox"/> control.
    /// </summary>
    /// <param name="selectedItem">The selected item</param>
    /// <param name="selectedIndex">The index of the selected item</param>
    /// <param name="previousSelection">The previous selected item</param>
    public class ComboBoxSelectionChangedEventArgs(object? selectedItem, int selectedIndex, object previousSelection) : EventArgs
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
