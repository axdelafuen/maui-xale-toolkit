using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Platform.TreeView;
using Microsoft.Maui.Handlers;

namespace Maui.XaleToolkit.Handlers.TreeView
{
    public partial class TreeViewHandler : ViewHandler<ITreeView, MauiTreeView>
    {
        protected override MauiTreeView CreatePlatformView()
        {
            throw new NotImplementedException();
        }

        #region Update Methods
        private void UpdateItemsSource()
        {
            throw new NotImplementedException();
        }

        private void UpdateSelectedItem()
        {
            throw new NotImplementedException();
        }

        private void UpdateTextColor()
        {
            throw new NotImplementedException();
        }

        private void UpdateFontSize()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
