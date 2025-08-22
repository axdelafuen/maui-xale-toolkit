using Android.Views;
using Android.Widget;
using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Collections;
using ListView = Android.Widget.ListView;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    public partial class TreeViewHandler : ViewHandler<ITreeView, ListView>
    {
        private RecursiveTreeViewAdapter? _adapter;

        protected override ListView CreatePlatformView()
        {
            var context = Context ?? throw new InvalidOperationException("Context cannot be null");
            return new ListView(context);
        }

        protected override void ConnectHandler(ListView platformView)
        {
            base.ConnectHandler(platformView);

            if (_adapter == null && VirtualView?.ItemsSource != null)
            {
                if (Context == null) return;
                _adapter = new RecursiveTreeViewAdapter(Context!, VirtualView);
                platformView.Adapter = _adapter;
                UpdateItemsSource();
                UpdateTextColor();
                UpdateFontSize();
            }

            platformView.ItemClick += OnItemClick;
        }

        protected override void DisconnectHandler(ListView platformView)
        {
            if (platformView != null)
            {
                platformView.ItemClick -= OnItemClick;
                platformView.Adapter = null;
            }

            _adapter?.Dispose();
            _adapter = null;
        }

        private void OnItemClick(object? sender, AdapterView.ItemClickEventArgs e)
        {
            if (VirtualView != null && _adapter != null)
            {
                var treeItem = _adapter.GetTreeItem(e.Position);
                
                if (treeItem.HasChildren)
                {
                    _adapter.UpdateFlatList(treeItem);
                    _adapter.NotifyDataSetChanged();
                }

                VirtualView.SelectedItem = treeItem.Data;
                _adapter.SelectedItem = treeItem.Data;
                _adapter.NotifyDataSetChanged();
                UpdateControlHeight();
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
                    PlatformView.Adapter = null;
                    _adapter?.Dispose();
                    _adapter = null;
                    return;
                }

                if (_adapter == null)
                {
                    _adapter = new RecursiveTreeViewAdapter(Context!, VirtualView);
                    PlatformView.Adapter = _adapter;
                }
                else
                {
                    _adapter.UpdateItemsSource(VirtualView.ItemsSource);
                }

                UpdateTextColor();
                UpdateFontSize();
                UpdateControlHeight();
            }
            catch (Exception) { }
        }

        private void UpdateSelectedItem()
        {
            if (PlatformView == null || VirtualView == null || _adapter == null)
                return;

            try
            {
                _adapter.SelectedItem = VirtualView.SelectedItem;
                _adapter.NotifyDataSetChanged();
            }
            catch (Exception) { }
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
                    _adapter.NotifyDataSetChanged();
                }
            }
            catch (Exception) { }
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
                    _adapter.NotifyDataSetChanged();
                }
            }
            catch (Exception) { }
        }

        private void UpdateControlHeight()
        {
            if (PlatformView == null || VirtualView == null || _adapter == null || _adapter.Count == 0)
                return;

            var itemHeight = _adapter.GetItemHeight();
            var visibleItems = _adapter.Count;
            VirtualView.UpdateHeight(itemHeight * visibleItems);
        }
        #endregion
    }

    #region Tree View Item
    /// <summary>
    /// Represents an item in the tree view with its hierarchical level and expansion state.
    /// </summary>
    internal class TreeViewItem
    {
        public object Data { get; set; }
        public int Level { get; set; }
        public bool IsExpanded { get; set; }
        public bool HasChildren { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewItem"/> class.
        /// </summary>
        /// <param name="data">The current object for the <see cref="TreeViewItem"/>.</param>
        /// <param name="level">The level of the tree.</param>
        internal TreeViewItem(object data, int level)
        {
            Data = data;
            Level = level;
            IsExpanded = false;
            HasChildren = TryGetChildren(data, out _);
        }

        private static bool TryGetChildren(object item, out IEnumerable children)
        {
            children = null!;

            if (item == null)
                return false;

            try
            {
                var type = item.GetType();
                var childrenProperty = type.GetProperty("Children");

                if (childrenProperty?.GetValue(item) is IEnumerable childItems)
                {
                    children = childItems;
                    foreach (var child in children)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        internal IEnumerable<object> GetChildren()
        {
            if (!HasChildren) yield break;

            var type = Data.GetType();
            var childrenProperty = type.GetProperty("Children");

            if (childrenProperty?.GetValue(Data) is IEnumerable children)
            {
                foreach (var child in children)
                {
                    if (child != null)
                        yield return child;
                }
            }

            yield break;
        }
    }
    #endregion

    #region Adapter
    internal class RecursiveTreeViewAdapter : BaseAdapter
    {
        private readonly Android.Content.Context _context;
        private IEnumerable? _itemsSource;
        private readonly List<TreeViewItem> _flatList = [];
        private readonly HashSet<object> _expandedItems = [];

        internal object? SelectedItem { get; set; }
        internal Color TextColor { get; set; } = Colors.Black;
        internal float FontSize { get; set; } = 14f;

        internal RecursiveTreeViewAdapter(Android.Content.Context context, ITreeView treeView)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _itemsSource = treeView.ItemsSource;
            RebuildFlatList();
        }

        internal void UpdateItemsSource(IEnumerable itemsSource)
        {
            _itemsSource = itemsSource;
            RebuildFlatList();
            NotifyDataSetChanged();
        }

        internal void UpdateFlatList(TreeViewItem currentTreeView)
        {
            var key = currentTreeView.Data;

            if (!_expandedItems.Remove(key))
                _expandedItems.Add(key);

            RebuildFlatList();
            NotifyDataSetChanged();
        }

        internal void RebuildFlatList()
        {
            _flatList.Clear();
            if (_itemsSource != null)
            {
                foreach (var item in _itemsSource)
                {
                    AddItemRecursively(item, 0);
                }
            }
        }

        internal void AddItemRecursively(object item, int level)
        {
            bool isExpanded = _expandedItems.Contains(item);

            var treeItem = new TreeViewItem(item, level)
            {
                IsExpanded = isExpanded
            };

            _flatList.Add(treeItem);

            if (isExpanded && treeItem.HasChildren)
            {
                foreach (var child in treeItem.GetChildren())
                {
                    AddItemRecursively(child, level + 1);
                }
            }
        }

        public override int Count => _flatList.Count;

        public override Java.Lang.Object GetItem(int position)
        {
            return Java.Lang.Integer.ValueOf(position);
        }

        internal TreeViewItem GetTreeItem(int position) => _flatList[position];

        public override long GetItemId(int position) => position;

        public override Android.Views.View? GetView(int position, Android.Views.View? convertView, ViewGroup? parent)
        {
            var treeItem = _flatList[position];

            LinearLayout? itemLayout;
            TextView? textView;
            TextView? expandIcon;

            if (convertView is LinearLayout existingLayout)
            {
                itemLayout = existingLayout;
                expandIcon = itemLayout.GetChildAt(0) as TextView;
                textView = itemLayout.GetChildAt(1) as TextView;
            }
            else
            {
                itemLayout = new LinearLayout(_context)
                {
                    Orientation = Orientation.Horizontal
                };
                itemLayout.SetVerticalGravity(GravityFlags.CenterVertical);

                expandIcon = new TextView(_context);
                textView = new TextView(_context);

                itemLayout.AddView(expandIcon);
                itemLayout.AddView(textView);
            }

            int indentationPx = (int)(treeItem.Level * 40 * (_context.Resources?.DisplayMetrics?.Density ?? 1));
            itemLayout.SetPadding(indentationPx + 16, 16, 16, 16);

            if (treeItem.HasChildren && expandIcon != null)
            {
                expandIcon.Text = treeItem.IsExpanded ? "▼ " : "▶ ";
                expandIcon.SetTextColor(TextColor.ToPlatform());
                expandIcon.SetTextSize(Android.Util.ComplexUnitType.Sp, FontSize * 0.8f);
                expandIcon.Visibility = ViewStates.Visible;
            }
            else if (expandIcon != null)
            {
                expandIcon.Text = "  ";
                expandIcon.Visibility = ViewStates.Invisible;
            }
            
            if (textView != null)
            {
                textView.Text = treeItem.Data?.ToString() ?? "";
                textView.SetTextColor(TextColor.ToPlatform());
                textView.SetTextSize(Android.Util.ComplexUnitType.Sp, FontSize);
            }

            return itemLayout;
        }

        internal double GetItemHeight()
        {
            if (_flatList.Count == 0)
                return 0;

            var view = GetView(0, null, null);
            view?.Measure(
                Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
            return view?.MeasuredHeight * 0.7 ?? 0;
        }
    }
    #endregion
}