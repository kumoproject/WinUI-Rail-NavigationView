using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace WinUIApp;

public sealed partial class DemoPage : Page
{
    public string PageTag { get; private set; } = "Home";

    public DemoPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        PageTag = e.Parameter as string ?? "Home";

        PageTitle.Text = GetTitle(PageTag);
        PageDescription.Text = GetDescription(PageTag);
    }

    public static string GetTitle(string tag) => tag switch
    {
        "Gallery" => "Gallery",
        "Models" => "Models",
        "Settings" => "Settings",
        _ => "Home",
    };

    private static string GetDescription(string tag) => tag switch
    {
        "Gallery" => "The rail style keeps primary destinations compact while leaving the content area open for gallery previews.",
        "Models" => "Use the same RailNavigationViewStyle in WinUI 3 apps by merging RailNavigationViewResources and applying the style explicitly.",
        "Settings" => "Footer navigation items participate in the same Frame history, so the title-bar back button can return to the previous page.",
        _ => "This WinUI demo loads CustomNavigationViewStyle.xaml from the Kumo.WinUI.NavigationView.RailStyle library and applies RailNavigationViewStyle directly.",
    };
}
