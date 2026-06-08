using Muxc = Microsoft.UI.Xaml.Controls;

namespace Kumo.WinUI.NavigationView.RailStyle.Demo;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        NavView.SelectedItem = NavView.MenuItems[0];
    }

    private void NavView_SelectionChanged(Muxc.NavigationView sender, Muxc.NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is Muxc.NavigationViewItem item && item.Content is string title)
        {
            PageTitle.Text = title;
        }
    }
}
