using Windows.UI.Xaml.Controls;
using Muxc = Microsoft.UI.Xaml.Controls;

namespace UwpApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame"/>.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
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
}
