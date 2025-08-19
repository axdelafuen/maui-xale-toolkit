[assembly: XmlnsDefinition(Constants.XamlNamespace, Constants.XaleToolkitNamespacePrefix + nameof(Maui.XaleToolkit.Views))]
[assembly: XmlnsDefinition(Constants.XamlNamespace, Constants.XaleToolkitNamespace)]

[assembly: Microsoft.Maui.Controls.XmlnsPrefix(Constants.XamlNamespace, "xale")]

static class Constants
{
    public const string XamlNamespace = "http://schemas.axdelafuen.com/2025/maui/xaletoolkit";
    public const string XaleToolkitNamespace = $"{nameof(Maui)}.{nameof(Maui.XaleToolkit)}";
    public const string XaleToolkitNamespacePrefix = $"{XaleToolkitNamespace}.";
}
