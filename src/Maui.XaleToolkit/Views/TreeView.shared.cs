using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Primitives;
using System.Collections;

namespace Maui.XaleToolkit.Views
{
    public partial class TreeView : View, ITreeView
    {
        #region Bindable Properties
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(TreeView), null);
        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(TreeView), default, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TreeView), Colors.Black);
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(TreeView), 14.0);
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
        #endregion

        public event EventHandler<TreeViewSelectedItemChangedEventArgs>? SelectedItemChanged;

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var treeView = (TreeView)bindable;
            if (treeView.ItemsSource != null && newValue != null)
            {
                treeView.OnSelectionChanged();
            }
        }

        protected virtual void OnSelectionChanged()
        {
            SelectedItemChanged?.Invoke(this, new TreeViewSelectedItemChangedEventArgs(SelectedItem));
        }
    }
}
