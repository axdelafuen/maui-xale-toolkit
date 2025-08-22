using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Primitives;
using System.Collections;
using System.Collections.Specialized;

namespace Maui.XaleToolkit.Views
{
    /// <summary>
    /// Represents a <see cref="ComboBox"/> control that allows users to select an item from a dropdown list.
    /// </summary>
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

        /// <inheritdoc/>
        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <inheritdoc/>
        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        /// <inheritdoc/>
        public int SelectedIndex { get; set; } = -1;

        /// <inheritdoc/>
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <inheritdoc/>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <inheritdoc/>
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <inheritdoc/>
        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        #endregion

        /// <inheritdoc/>
        public event EventHandler<ComboBoxSelectedItemChangedEventArgs>? SelectedItemChanged;

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var comboBox = (ComboBox)bindable;

            if (oldValue is INotifyCollectionChanged oldCollection)
            {
                oldCollection.CollectionChanged -= comboBox.OnCollectionChanged;
            }

            if (newValue is INotifyCollectionChanged newCollection)
            {
                newCollection.CollectionChanged += comboBox.OnCollectionChanged;
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var comboBox = (ComboBox)bindable;
            comboBox._previousSelection = oldValue;

            if (comboBox.ItemsSource != null && newValue != null)
            {
                var index = -1;
                for (int i = 0; i < comboBox.ItemsSource.Count; i++)
                {
                    if (Equals(comboBox.ItemsSource[i], newValue))
                    {
                        index = i;
                        break;
                    }
                }

                if (comboBox.SelectedIndex != index)
                {
                    comboBox.SelectedIndex = index;
                }
            }
            else if (newValue == null)
            {
                comboBox.SelectedIndex = -1;
            }

            comboBox.OnSelectionChanged();
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (SelectedIndex >= ItemsSource?.Count)
            {
                SelectedIndex = -1;
                SelectedItem = null;
            }
        }

        /// <summary>
        /// Raises the <see cref="SelectedItemChanged"/> event.
        /// </summary>
        protected virtual void OnSelectionChanged()
        {
            if (_previousSelection is object previous)
                SelectedItemChanged?.Invoke(this, new ComboBoxSelectedItemChangedEventArgs(SelectedItem, SelectedIndex, previous));
        }
    }
}
