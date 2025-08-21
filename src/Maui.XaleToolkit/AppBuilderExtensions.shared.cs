using Maui.XaleToolkit.Handlers.ComboBox;
using Maui.XaleToolkit.Handlers.TreeView;
using Maui.XaleToolkit.Views;
using System.Runtime.Versioning;

namespace Maui.XaleToolkit
{
    /// <summary>
    /// Extensions for MauiAppBuilder
    /// </summary>
    [SupportedOSPlatform("iOS15.0")]
    [SupportedOSPlatform("MacCatalyst15.0")]
    [SupportedOSPlatform("Android21.0")]
    [SupportedOSPlatform("Windows10.0.17763")]
    public static class AppBuilderExtension
    {
        /// <summary>
        /// Registers the .NET MAUI XaleToolkit handlers with the MauiAppBuilder.
        /// </summary>
        /// <param name="builder">The <see cref="MauiAppBuilder"/> to configure.</param>
        /// <returns>The configured <see cref="MauiAppBuilder"/>.</returns>
        public static MauiAppBuilder UseMauiXaleToolkit(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<ComboBox, ComboBoxHandler>();
                handlers.AddHandler<TreeView, TreeViewHandler>();
            });

            return builder;
        }
    }
}
