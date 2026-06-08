# Kumo.Uwp.NavigationView.RailStyle

Rail-style `NavigationView` templates and helper attached properties for UWP with WinUI 2.

## Usage

Merge the resource dictionary after `XamlControlsResources`:

```xml
<ResourceDictionary.MergedDictionaries>
    <muxc:XamlControlsResources xmlns:muxc="using:Microsoft.UI.Xaml.Controls" />
    <ResourceDictionary Source="ms-appx:///Kumo.Uwp.NavigationView.RailStyle/Styles/CustomNavigationViewStyle.xaml" />
</ResourceDictionary.MergedDictionaries>
```

Apply the style explicitly:

```xml
<muxc:NavigationView Style="{StaticResource RailNavigationViewStyle}" />
```

Optional item helpers live in `Kumo.WinUI.NavigationView.RailStyle.Helpers.NavItemIconHelper`.

## Source Notice

`Styles/CustomNavigationViewStyle.xaml` is derived from Microsoft AI Dev Gallery's `NavigationView.xaml`, licensed under the MIT License. The UWP version keeps the template body aligned with the WinUI version, with WinUI 2 namespace prefixes where UWP XAML requires them.
