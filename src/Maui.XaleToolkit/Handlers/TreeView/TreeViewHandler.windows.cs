using Maui.XaleToolkit.Interfaces;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Controls;
using System.Collections;
using WinTreeView = Microsoft.UI.Xaml.Controls.TreeView;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    public partial class TreeViewHandler : ViewHandler<ITreeView, WinTreeView>
    {
        protected override WinTreeView CreatePlatformView()
        {
            var treeView = new WinTreeView
            {
                HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Stretch,
                VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center,
                BorderThickness = new Microsoft.UI.Xaml.Thickness(1),
                Padding = new Microsoft.UI.Xaml.Thickness(8, 4, 8, 4),
                CornerRadius = new Microsoft.UI.Xaml.CornerRadius(4),
            };

            treeView.ItemInvoked += OnItemInvoked;

            return treeView;
        }

        protected override void ConnectHandler(WinTreeView platformView)
        {
            base.ConnectHandler(platformView);

            if (VirtualView != null)
            {
                UpdateItemsSource();
                UpdateItemTemplate();
                UpdateSelectedItem();
                UpdateTextColor();
                UpdateFontSize();
            }
        }

        protected override void DisconnectHandler(WinTreeView platformView)
        {
            base.DisconnectHandler(platformView);

            if (platformView != null)
            {
                platformView.ItemInvoked -= OnItemInvoked;
            }
        }
        private void OnItemInvoked(WinTreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is TreeViewNode node)
            {
                VirtualView.SelectedItem = node.Content;
            }
        }

        private static TreeViewNode CreateNode(object item)
        {
            var node = new TreeViewNode
            {
                Content = item,
                IsExpanded = false,
            };

            var childrenProp = item?.GetType().GetProperty("Children");
            if (childrenProp?.GetValue(item) is IList children)
            {
                foreach (var child in children)
                    node.Children.Add(CreateNode(child));
            }

            return node;
        }

        private static TreeViewNode? FindNode(TreeViewNode node, object target)
        {
            if (node.Content == target || (node.Content as ContentControl)?.Content == target)
                return node;

            foreach (var child in node.Children)
            {
                var result = FindNode(child, target);
                if (result != null)
                    return result;
            }

            return null;
        }

        #region Update Methods
        private void UpdateItemsSource()
        {
            if (PlatformView == null || VirtualView?.ItemsSource == null)
                return;

            if (VirtualView.ItemsSource is IList items)
            {
                PlatformView.RootNodes.Clear();

                foreach (var item in items)
                {
                    PlatformView.RootNodes.Add(CreateNode(item));
                }
            }
        }

        private void UpdateItemTemplate()
        {
            UpdateItemsSource();
        }

        private void UpdateSelectedItem()
        {
            if (PlatformView == null || VirtualView?.SelectedItem == null)
                return;

            foreach (var node in PlatformView.RootNodes)
            {
                var selected = FindNode(node, VirtualView.SelectedItem);
                if (selected != null)
                {
                    PlatformView.SelectedNode = selected;
                    break;
                }
            }
        }

        private void UpdateTextColor()
        {
            UpdateItemsSource();
        }

        private void UpdateFontSize()
        {
            UpdateItemsSource();
        }
        #endregion
    }
}
