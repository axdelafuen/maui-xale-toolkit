using Maui.XaleToolkit.Samples.Models;
using Maui.XaleToolkit.Samples.Stub;

namespace Maui.XaleToolkit.Samples.Samples.ComboBox
{
    public partial class ComboBoxSamplePage : ContentPage
    {
        private TestObject? _selectedItem;
        public TestObject? SelectedItem
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

        public IEnumerable<TestObject> ItemsSource => StubedModel.GetItems();

        public ComboBoxSamplePage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
