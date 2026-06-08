using System;
using Microsoft.UI.Xaml;

namespace Kumo.WinUI.NavigationView.RailStyle;

public sealed class RailNavigationViewResources : ResourceDictionary
{
    public RailNavigationViewResources()
    {
        Source = new Uri("ms-appx:///Kumo.WinUI.NavigationView.RailStyle/Styles/CustomNavigationViewStyle.xaml");
    }
}
