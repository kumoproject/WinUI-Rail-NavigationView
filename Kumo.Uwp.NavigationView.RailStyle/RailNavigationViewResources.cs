using System;
using Windows.UI.Xaml;

namespace Kumo.WinUI.NavigationView.RailStyle;

public sealed partial class RailNavigationViewResources : ResourceDictionary
{
    public RailNavigationViewResources()
    {
        Source = new Uri("ms-appx:///Kumo.Uwp.NavigationView.RailStyle/Styles/CustomNavigationViewStyle.xaml");
    }
}
