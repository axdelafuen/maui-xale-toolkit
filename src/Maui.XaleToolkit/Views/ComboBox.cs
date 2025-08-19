using Maui.XaleToolkit.Interfaces;
using System.Collections;
using System.Collections.Specialized;

namespace Maui.XaleToolkit.Views.ComboBox
{
    public partial class ComboBox : View, IComboBox
    {
        private object? _previousSelection;

        #region Bindable Properties

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(ComboBox), default(IList), propertyChanged: OnItemsSourceChanged);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(ComboBox), default, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ComboBox), string.Empty);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ComboBox), Colors.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ComboBox), 14.0);
        public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(ComboBox), true);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of items displayed.
        /// </summary>
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the currently selected item.
        /// </summary>
        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <summary>
        /// Gets or sets the index of the currently selected item.
        /// </summary>
        public int SelectedIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets the placeholder text displayed when no item is selected.
        /// </summary>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// Gets or sets the color of the text displayed.
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size of the text displayed.
        /// </summary>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ComboBox"/> is enabled.
        /// </summary>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        #endregion

        /// <summary>
        /// Occurs when the selection changes.
        /// </summary>
        public event EventHandler<ComboBoxSelectionChangedEventArgs>? SelectionChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="ComboBox"/>.
        /// </summary>
        public ComboBox() { }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var spinner = (ComboBox)bindable;

            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= spinner.OnCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += spinner.OnCollectionChanged;
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var spinner = (ComboBox)bindable;
            spinner._previousSelection = oldValue;

            if (spinner.ItemsSource != null && newValue != null)
            {
                var index = -1;
                for (int i = 0; i < spinner.ItemsSource.Count; i++)
                {
                    if (Equals(spinner.ItemsSource[i], newValue))
                    {
                        index = i;
                        break;
                    }
                }

                if (spinner.SelectedIndex != index)
                {
                    spinner.SelectedIndex = index;
                }
            }
            else if (newValue == null)
            {
                spinner.SelectedIndex = -1;
            }

            spinner.OnSelectionChanged();
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (SelectedIndex >= ItemsSource?.Count)
            {
                SelectedIndex = -1;
                SelectedItem = null;
            }
        }

        protected virtual void OnSelectionChanged()
        {
            if (_previousSelection is object previous)
                SelectionChanged?.Invoke(this, new ComboBoxSelectionChangedEventArgs(SelectedItem, SelectedIndex, previous));
        }
    }
}