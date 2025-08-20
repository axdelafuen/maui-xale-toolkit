using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Platform.TreeView;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml.Controls;
using System.Collections;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    public partial class TreeViewHandler : ViewHandler<ITreeView, MauiTreeView>
    {
        protected override MauiTreeView CreatePlatformView()
        {
            var treeView =  new MauiTreeView();

            treeView.ItemInvoked += OnItemInvoked;

            return treeView;
        }

        protected override void ConnectHandler(MauiTreeView platformView)
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

        protected override void DisconnectHandler(MauiTreeView platformView)
        {
            base.DisconnectHandler(platformView);

            if (platformView != null)
            {
                platformView.ItemInvoked -= OnItemInvoked;
            }
        }

        private void OnItemInvoked(Microsoft.UI.Xaml.Controls.TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is TreeViewNode node)
            {
                VirtualView.SelectedItem = node.Content;
            }
        }

        private TreeViewNode CreateNode(object item)
        {
            var node = new TreeViewNode
            {
                Content = item,
                IsExpanded = true
            };

            if (VirtualView?.ItemTemplate != null && MauiContext != null)
            {
                var mauiContent = (View)VirtualView.ItemTemplate.CreateContent();
                mauiContent.BindingContext = item;
                
                var nativeView = mauiContent.ToPlatform(MauiContext);

                node.Content = nativeView;
            }
            else
            {
                node.Content = new TextBlock
                {
                    Text = item?.ToString(),
                    Foreground = VirtualView?.TextColor.ToPlatform() ?? Colors.Black.ToPlatform(),
                    FontSize = VirtualView?.FontSize ?? 14.0
                };
            }

            var childrenProp = item?.GetType().GetProperty("Children");
            if (childrenProp?.GetValue(item) is IList children)
            {
                foreach (var child in children)
                    node.Children.Add(CreateNode(child));
            }

            return node;
        }

        private TreeViewNode? FindNode(TreeViewNode node, object target)
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
