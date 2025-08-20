using Maui.XaleToolkit.Primitives;
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
}
