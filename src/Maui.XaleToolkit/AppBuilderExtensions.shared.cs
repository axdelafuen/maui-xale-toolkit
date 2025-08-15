#if ANDROID
using Maui.XaleToolkit.Handlers.ComboBox;
using Maui.XaleToolkit.Interfaces;
using Maui.XaleToolkit.Views.ComboBox;
#endif

namespace Maui.XaleToolkit
{
    public static class AppBuilderExtension
    {
        public static MauiAppBuilder UseMauiXaleToolkit(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler<ComboBox, ComboBoxHandler>();

                Microsoft.Maui.Handlers.ViewHandler.ViewMapper.AppendToMapping("ComboBox", (handler, view) =>
                {
                    if (handler is ComboBoxHandler comboBoxHandler && view is IComboBox comboBox)
                    {
                        ComboBoxHandler.MapItemsSource(comboBoxHandler, comboBox);
                        ComboBoxHandler.MapSelectedIndex(comboBoxHandler, comboBox);
                        ComboBoxHandler.MapTitle(comboBoxHandler, comboBox);
                        ComboBoxHandler.MapTextColor(comboBoxHandler, comboBox);
                        ComboBoxHandler.MapFontSize(comboBoxHandler, comboBox);
                        ComboBoxHandler.MapIsEnabled(comboBoxHandler, comboBox);
                    }
                });
#endif
            });

            return builder;
        }
    }
}