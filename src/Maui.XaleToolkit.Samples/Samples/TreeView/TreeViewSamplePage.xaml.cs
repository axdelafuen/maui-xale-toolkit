using Maui.XaleToolkit.Samples.Models;
using Maui.XaleToolkit.Samples.Stub;

namespace Maui.XaleToolkit.Samples.Samples.TreeView
{
    public partial class TreeViewSamplePage : ContentPage
    {
        private TestTreeNode? _selectedItem;
        public TestTreeNode? SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }

        public IList<TestTreeNode> ItemsSource => StubedTreeNode.GetNodes();

        public TreeViewSamplePage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            SelectedItem = null;
        }
    }
}
