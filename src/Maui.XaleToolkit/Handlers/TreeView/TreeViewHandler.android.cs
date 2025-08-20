using Android.Views;
using Android.Widget;
using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Collections;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    public partial class TreeViewHandler : ViewHandler<ITreeView, ExpandableListView>
    {
        private TreeViewAdapter? _adapter;

        protected override ExpandableListView CreatePlatformView()
        {
            var context = Context ?? throw new InvalidOperationException("Context cannot be null");
            return new ExpandableListView(context, null, Android.Resource.Attribute.ExpandableListPreferredChildIndicatorLeft, ExpandableListView.ChildIndicatorInherit);
        }

        protected override void ConnectHandler(ExpandableListView platformView)
        {
            base.ConnectHandler(platformView);

            if (_adapter == null && VirtualView?.ItemsSource != null)
            {
                if (Context == null) return;
                _adapter = new TreeViewAdapter(Context!, VirtualView);
                platformView.SetAdapter(_adapter);
                UpdateItemsSource();
                UpdateTextColor();
                UpdateFontSize();
            }

            platformView.ChildClick += OnChildClick;
            platformView.GroupClick += OnGroupClick;
        }

        protected override void DisconnectHandler(ExpandableListView platformView)
        {
            if (platformView != null)
            {
                platformView.ChildClick -= OnChildClick;
                platformView.GroupClick -= OnGroupClick;
                platformView.SetAdapter(null);
            }

            _adapter?.Dispose();
            _adapter = null;
        }

        private void OnChildClick(object? sender, ExpandableListView.ChildClickEventArgs e)
        {
            if (VirtualView != null && _adapter != null)
            {
                var selectedItem = _adapter.GetChild(e.GroupPosition, e.ChildPosition);
                VirtualView.SelectedItem = selectedItem;
            }
        }

        private void OnGroupClick(object? sender, ExpandableListView.GroupClickEventArgs e)
        {
            if (VirtualView != null && _adapter != null)
            {
                var selectedItem = _adapter.GetGroup(e.GroupPosition);
                VirtualView.SelectedItem = selectedItem;
            }
        }

        #region Update Methods
        private void UpdateItemsSource()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                if (VirtualView.ItemsSource == null)
                {
                    PlatformView.SetAdapter(null);
                    _adapter?.Dispose();
                    _adapter = null;
                    return;
                }

                if (_adapter == null)
                {
                    _adapter = new TreeViewAdapter(Context!, VirtualView);
                    PlatformView.SetAdapter(_adapter);
                }
                else
                {
                    _adapter.UpdateItemsSource(VirtualView.ItemsSource);
                }

                UpdateTextColor();
                UpdateFontSize();
            }
            catch (Exception){}
        }

        private void UpdateSelectedItem()
        {
            if (PlatformView == null || VirtualView == null || _adapter == null)
                return;

            try
            {
                var selectedItem = VirtualView.SelectedItem;
                if (selectedItem == null)
                {
                    _adapter.SelectedItem = null;
                    return;
                }

                _adapter.SelectedItem = selectedItem;

                var itemsSource = VirtualView.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    int groupIndex = 0;
                    foreach (var group in itemsSource)
                    {
                        if (ReferenceEquals(group, selectedItem))
                        {
                            PlatformView.SetSelectedGroup(groupIndex);
                            return;
                        }

                        if (TryGetChildren(group, out var children))
                        {
                            int childIndex = 0;
                            foreach (var child in children)
                            {
                                if (ReferenceEquals(child, selectedItem))
                                {
                                    PlatformView.ExpandGroup(groupIndex);
                                    PlatformView.SetSelectedChild(groupIndex, childIndex, true);
                                    return;
                                }
                                childIndex++;
                            }
                        }
                        groupIndex++;
                    }
                }
            }
            catch (Exception) {}
        }

        private void UpdateTextColor()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                if (_adapter != null && VirtualView.TextColor != null)
                {
                    var androidColor = VirtualView.TextColor.ToPlatform();
                    _adapter.TextColor = androidColor.ToColor();
                }
            }
            catch (Exception) {}
        }

        private void UpdateFontSize()
        {
            if (PlatformView == null || VirtualView == null)
                return;

            try
            {
                if (_adapter != null && VirtualView.FontSize > 0)
                {
                    _adapter.FontSize = (float)VirtualView.FontSize;
                }
            }
            catch (Exception) {}
        }
        #endregion

        #region Helper Methods
        private bool TryGetChildren(object item, out IEnumerable children)
        {
            children = null!;

            if (item == null)
                return false;

            // Try to get children using common property names
            var type = item.GetType();
            var childrenProperty = type.GetProperty("Children") ??
                                 type.GetProperty("Items") ??
                                 type.GetProperty("SubItems");

            if (childrenProperty != null && typeof(IEnumerable).IsAssignableFrom(childrenProperty.PropertyType))
            {
                children = childrenProperty.GetValue(item) as IEnumerable;
                return children != null;
            }

            return false;
        }
        #endregion
    }

    #region Adapter
    internal class TreeViewAdapter : BaseExpandableListAdapter
    {
        private readonly ITreeView _treeView;
        private readonly Android.Content.Context _context;
        private IEnumerable? _itemsSource;

        public object? SelectedItem { get; set; }
        public Color TextColor { get; set; } = Colors.Black;
        public float FontSize { get; set; } = 14f;

        public TreeViewAdapter(Android.Content.Context context, ITreeView treeView)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _treeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            _itemsSource = treeView.ItemsSource;
        }

        public void UpdateItemsSource(IEnumerable itemsSource)
        {
            _itemsSource = itemsSource;
            NotifyDataSetChanged();
        }

        public override int GroupCount => _itemsSource?.Cast<object>().Count() ?? 0;

        public override bool HasStableIds => false;

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var group = GetGroup(groupPosition);
            if (TryGetChildren(group, out var children))
            {
                return children.Cast<object>().ElementAtOrDefault(childPosition).ToJavaObject();
            }
            return new Java.Lang.String("No Data");
        }

        public override long GetChildId(int groupPosition, int childPosition) => childPosition;

        public override int GetChildrenCount(int groupPosition)
        {
            var group = GetGroup(groupPosition);
            if (TryGetChildren(group, out var children))
            {
                return children.Cast<object>().Count();
            }
            return 0;
        }

        public override Android.Views.View? GetChildView(int groupPosition, int childPosition, bool isLastChild, Android.Views.View? convertView, ViewGroup? parent)
        {
            var child = GetChild(groupPosition, childPosition);
            var childObj = child.ToNetObject();

            var textView = convertView as TextView ?? new TextView(_context);
            textView.Text = childObj?.ToString() ?? "";
            //textView.SetTextColor(TextColor);
            textView.SetTextSize(Android.Util.ComplexUnitType.Sp, FontSize);
            textView.SetPadding(60, 16, 16, 16); // Indent child items

            if (ReferenceEquals(childObj, SelectedItem))
            {
                textView.SetBackgroundColor(Colors.LightGray.ToPlatform());
            }
            else
            {
                textView.SetBackgroundColor(Colors.Transparent.ToPlatform());
            }

            return textView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            if (_itemsSource != null)
            {
                return _itemsSource.Cast<object>().ElementAtOrDefault(groupPosition).ToJavaObject();
            }
            return new Java.Lang.String("No Data");
        }

        public override long GetGroupId(int groupPosition) => groupPosition;

        public override Android.Views.View? GetGroupView(int groupPosition, bool isExpanded, Android.Views.View? convertView, ViewGroup? parent)
        {
            var group = GetGroup(groupPosition);
            var groupObj = group.ToNetObject();

            var textView = convertView as TextView ?? new TextView(_context);
            textView.Text = groupObj?.ToString() ?? "";
            //textView.SetTextColor(TextColor);
            textView.SetTextSize(Android.Util.ComplexUnitType.Sp, FontSize);
            textView.SetPadding(16, 16, 16, 16);

            // Highlight if selected
            if (ReferenceEquals(groupObj, SelectedItem))
            {
                textView.SetBackgroundColor(Colors.LightGray.ToPlatform());
            }
            else
            {
                textView.SetBackgroundColor(Colors.Transparent.ToPlatform());
            }

            return textView;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition) => true;

        private bool TryGetChildren(Java.Lang.Object javaGroup, out IEnumerable children)
        {
            children = null!;
            var group = javaGroup.ToNetObject();

            if (group == null)
                return false;

            var type = group.GetType();
            var childrenProperty = type.GetProperty("Children") ??
                                 type.GetProperty("Items") ??
                                 type.GetProperty("SubItems");

            if (childrenProperty != null && typeof(IEnumerable).IsAssignableFrom(childrenProperty.PropertyType))
            {
                children = childrenProperty.GetValue(group) as IEnumerable;
                return children != null;
            }

            return false;
        }
    }
    #endregion

    #region Extensions
    internal static class ObjectExtensions
    {
        public static Java.Lang.Object ToJavaObject(this object obj)
        {
            if (obj == null) return new Java.Lang.String("No Data");
            if (obj is Java.Lang.Object javaObj) return javaObj;
            return new JavaObjectWrapper(obj);
        }

        public static object? ToNetObject(this Java.Lang.Object javaObj)
        {
            if (javaObj is JavaObjectWrapper wrapper)
                return wrapper.WrappedObject;
            return javaObj;
        }
    }

    internal class JavaObjectWrapper : Java.Lang.Object
    {
        public object WrappedObject { get; }

        public JavaObjectWrapper(object obj)
        {
            WrappedObject = obj ?? throw new ArgumentNullException(nameof(obj));
        }

        public override string ToString() => WrappedObject.ToString() ?? "";
    }
    #endregion
}
