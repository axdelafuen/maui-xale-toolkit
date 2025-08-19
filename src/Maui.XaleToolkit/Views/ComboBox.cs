﻿using Maui.XaleToolkit.Interfaces;
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

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public object? SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public int SelectedIndex { get; set; } = -1;

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public new bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        #endregion

        public event EventHandler<ComboBoxSelectionChangedEventArgs>? SelectionChanged;

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