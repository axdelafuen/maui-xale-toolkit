#if IOS || MACCATALYST
using Microsoft.Maui.Handlers;
using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Extensions.ComboBox;

namespace Maui.XaleToolkit.Handlers.ComboBox
{
    public partial class ComboBoxHandler : ViewHandler<IComboBox, MaciosComboBox>
    {
    }
}
#endif