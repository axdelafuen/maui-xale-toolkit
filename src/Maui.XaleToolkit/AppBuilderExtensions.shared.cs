using Maui.XaleToolkit.Handlers.ComboBox;
using Maui.XaleToolkit.Views.ComboBox;

namespace Maui.XaleToolkit
{
    public static class AppBuilderExtension
    {
        public static MauiAppBuilder UseMauiXaleToolkit(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<ComboBox, ComboBoxHandler>();
            });

            return builder;
        }
    }
}