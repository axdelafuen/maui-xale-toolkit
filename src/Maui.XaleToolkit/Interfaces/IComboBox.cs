using System.Collections;

namespace Maui.XaleToolkit.Interfaces
{
    public interface IComboBox : IView
    {
        /// <summary>
        /// Collection of items to display in the spinner
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
        /// Text color for the spinner
        /// </summary>
        Color TextColor { get; set; }

        /// <summary>
        /// Font size for the spinner text
        /// </summary>
        double FontSize { get; set; }

        /// <summary>
        /// Whether the spinner is enabled
        /// </summary>
        new bool IsEnabled { get; set; }

        /// <summary>
        /// Event fired when selection changes
        /// </summary>
        event EventHandler<ComboBoxSelectionChangedEventArgs> SelectionChanged;
    }

    public class ComboBoxSelectionChangedEventArgs(object? selectedItem, int selectedIndex, object previousSelection) : EventArgs
    {
        public object? SelectedItem { get; } = selectedItem;
        public int SelectedIndex { get; } = selectedIndex;
        public object PreviousSelection { get; } = previousSelection;
    }
}
